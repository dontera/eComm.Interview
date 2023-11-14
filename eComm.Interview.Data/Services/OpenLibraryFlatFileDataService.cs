using DelimitedDataParser;
using eComm.Interview.Data.Helpers;
using eComm.Interview.Data.Models;
using eComm.Interview.Data.Models.OpenLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static eComm.Interview.Data.Helpers.Enums;

namespace eComm.Interview.Data.Services
{
    public class OpenLibraryFlatFileDataService
    {
        public List<FlatFileWork> Records;

        private const string _baseJsonUrl = "https://openlibrary.org";
        private static HttpClient _sharedJsonClient = new()
        {
            BaseAddress = new Uri(_baseJsonUrl)
        };

        private const string _baseCoverUrl = "https://covers.openlibrary.org";
        private static HttpClient _sharedCoverClient = new ()
        {
            BaseAddress = new Uri(_baseCoverUrl)
        };

        public OpenLibraryFlatFileDataService() 
        {
            this.Records = new List<FlatFileWork>();
        }

        public async Task<List<FetchResult>> FetchResults(string? lookupRating, string? lookupWorkKey)
        {
            List<FetchResult> results = new List<FetchResult>();

            if (lookupRating == null && lookupWorkKey == null) 
            {
                // get works count by distinct date
                results.Add(await WriteResults(FetchType.TotalsByDate, null));
            }

            if (lookupRating != null)
            {
                // get works by rating
                results.Add(await WriteResults(FetchType.WorksByRating, lookupRating));
            }

            if (lookupWorkKey != null)
            {
                // get work by key
                results.Add(await WriteResults(FetchType.WorkByKey, lookupWorkKey));
            }

            return results;
        }

        private async Task<FetchResult> WriteResults(FetchType fetchType, string? arg)
        {
            FetchResult result = null;

            switch (fetchType)
            {
                case FetchType.TotalsByDate:
                    result = await GetTotalsByDate();
                    break;

                case FetchType.WorksByRating:
                    if (arg == null)
                        throw new ArgumentException("Missing Argument", "Rating");
                    
                    int rating = Int32.Parse(arg);
                    result = await GetWorksByRating(rating);                    
                    break;

                case FetchType.WorkByKey:
                    if (arg == null)
                        throw new ArgumentException("Missing Argument", "WorkKey");

                    result = await GetWorkByKey(arg);                        
                    break;
            }

            return result;
        }

        private async Task<FetchResult> GetWorkByKey(string workkey)
        {
            var filename = $"{FilesHelper.RandomString(7)}.json";

            // exit if no work found for key
            var workkeyfound = this.Records.Any(r => r.WorkKey == workkey);
            if (!workkeyfound)
            {
                return new FetchResult
                {
                    FetchType = FetchType.WorkByKey,
                    Success = false,
                    Message = "Work not found"
                };
            }

            var workrecord = this.Records.First(r => r.WorkKey.Equals(workkey));
            bool useEdition = !string.IsNullOrEmpty(workrecord.EditionKey);

            bool nosubject = false;
            bool nocover = false;

            if (useEdition)
            {
                // by Edition Key
                var editiondata = await GetJsonUrlResult<Edition>(url: workrecord.EditionKey);
                var workdata = await GetJsonUrlResult<Work>(url: workrecord.WorkKey);
                var authorData = await GetAuthors(editiondata.Authors.Select(a => a.Key).ToList());

                var subject = editiondata.Subjects != null && editiondata.Subjects.Count > 0 ? 
                                editiondata.Subjects.First() : 
                                workdata.Subjects != null && workdata.Subjects.Count > 0 ? 
                                    workdata.Subjects.First() : 
                                    null;
                nosubject = subject == null;

                await WriteResults(filename, new {
                    title = editiondata.Title,
                    first_subject = subject,
                    author_name = authorData.FirstOrDefault()?.Name,
                });

                if (editiondata.Covers != null && editiondata.Covers.Any())
                    await GetCover(editiondata.Covers.First().Value, filename);
                else
                    nocover = true;
            }
            else
            {
                // by Work Key
                var workdata = await GetJsonUrlResult<Work>(url: workrecord.WorkKey);
                var authorData = await GetAuthors(workdata.Authors.Select(a => a.Author.Key).ToList());

                var subject = workdata.Subjects != null && workdata.Subjects.Count > 0 ? 
                                    workdata.Subjects.First() : 
                                    null;
                nosubject = subject == null;

                await WriteResults(filename, new
                {
                    title = workdata.Title,
                    first_subject = subject,
                    author_name = authorData.FirstOrDefault()?.Name,
                });

                if (workdata.Covers != null && workdata.Covers.Any())
                    await GetCover(workdata.Covers.First().Value, filename);
                else
                    nocover = true;
            }

            string message = null;
            if (nocover) message += "No Cover. ";
            if (nosubject) message += "No Subject.";

            return new FetchResult
            {
                FetchType = FetchType.WorkByKey,
                Success = true,
                FileName = filename,
                Message = message
            };
        }

        private async Task GetCover(int coverid, string filename)
        {
            var filenamepart = Path.GetFileNameWithoutExtension(filename);
            var coverbytes = await _sharedCoverClient.GetByteArrayAsync($"/b/id/{coverid}-M.jpg");

            Directory.CreateDirectory("output");
            File.WriteAllBytes($"output/{filenamepart}.jpg", coverbytes);
        }

        private async Task<List<AuthorRecord>> GetAuthors(List<string> authors)
        {
            var result = new List<AuthorRecord>();

            foreach (var authorkey in authors)
            {
                result.Add(await GetJsonUrlResult<AuthorRecord>(authorkey));
            }

            return result;
        }

        private async Task<T> GetJsonUrlResult<T>(string url)
        {
            string urlappend = $"{url}.json?format=json";
            //Console.WriteLine($"Getting JSON result for url: {_sharedClient.BaseAddress}{urlappend}");

            var result = await _sharedJsonClient.GetFromJsonAsync<T>(urlappend);            
            return result;
        }

        private async Task<FetchResult> GetWorksByRating(int rating)
        {
            var filename = $"{FilesHelper.RandomString(7)}.json";

            // get works by rating ordered by Date asc
            List<string> orderedworksbyrating = this.Records.Where(r => r.Rating == rating).OrderBy(r => r.Date).Select(r => r.WorkKey).ToList();

            await WriteResults(filename, orderedworksbyrating);

            return new FetchResult
            {
                FetchType= FetchType.WorksByRating,
                Success = true,
                FileName = filename
            };
        }

        private async Task<FetchResult> GetTotalsByDate()
        {
            var filename = $"{FilesHelper.RandomString(7)}.json";

            // get distinct Dates
            List<DateTime> dates = this.Records.Select(r => r.Date).Distinct().ToList();

            // get counts by date
            Dictionary<DateTime, int> counts = new Dictionary<DateTime, int>();
            foreach (var date in dates)
            {
                counts[date] = this.Records.Where(r => r.Date == date).Count();
            }

            await WriteResults(filename, counts);

            return new FetchResult
            {
                FetchType = FetchType.TotalsByDate,
                Success = true,
                FileName = filename
            };
        }

        private async Task WriteResults(string filename, object results)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var output = JsonSerializer.Serialize(results, options); ;

            try
            {
                Directory.CreateDirectory("output");
                await File.WriteAllTextAsync($@"output\{filename}", output);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Error while saving results file:");
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<bool> LoadDataFile (string path)
        {
            var fullPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), path);
            if (!File.Exists(fullPath))
            {
                Console.WriteLine($"Error: DataFile not found at {path}");
                return false;
            }

            Console.WriteLine("File found, parsing");

            Parser parser = new Parser
            {
                FieldSeparator = '\t',
                UseFirstRowAsColumnHeaders = false,
                TrimColumnHeaders = true
            };

            using var stream = File.OpenText(fullPath);
            var reader = parser.ParseReader(stream);

            while (await reader.ReadAsync())
            {
                this.Records.Add(new FlatFileWork
                {
                    WorkKey = (string)reader[0],
                    EditionKey = (string)reader[1],
                    Rating = Int32.Parse((string)reader[2]),
                    Date = DateTime.Parse((string)reader[3])
                });
            }

            return true;
        }
    }
}
