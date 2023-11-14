using eComm.Interview.Data.Services;

using eComm.Interview.Data.Models;
using System.Reflection;
using System.IO;
using System.Text;
using System.Reflection.PortableExecutable;
using DelimitedDataParser;

namespace eComm.Interview.ConsoleApp
{
    public class App
    {
        private OpenLibraryFlatFileDataService _dataService;
        private const string _defaultFilePath = @"data\ol_dump_ratings_2023-10-31.txt";
        private string _dataFilePath;

        public string? LookupRating;
        public string? LookupWorkKey;

        public App(string dataFilePath = _defaultFilePath)
        {
            _dataFilePath = dataFilePath;
            this._dataService = new OpenLibraryFlatFileDataService();            
        }

        public void GetDataFilePathInput()
        {
            Console.WriteLine($"Input Data File path (press enter for default: {_defaultFilePath}):");
            var argPath = Console.ReadLine();
            if (!string.IsNullOrEmpty(argPath))
                this._dataFilePath = argPath;
        }

        public async Task<bool> LoadDataFile()
        {
            try
            {
                return await this._dataService.LoadDataFile(this._dataFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Unhandled Exception while loading the data file:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            
        }

        public int GetRecordCount()
        {
            try
            {
                return (this._dataService.Records ?? new List<FlatFileWork>()).Count;
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Unhandled Exception while getting record count:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return 0;
            }            
        }

        public void GetArguments()
        {
            Console.WriteLine();
            Console.WriteLine("Input Rating to lookup (press enter for default: null)");
            LookupRating = Console.ReadLine();
            if (LookupRating == string.Empty)
                LookupRating = null;

            Console.WriteLine("Input Work Key to lookup (press enter for default: null)");
            LookupWorkKey = Console.ReadLine();
            if (LookupWorkKey == string.Empty)
                LookupWorkKey = null;
        }

        public async Task GetDataResult()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine($"Fetching results for Rating:{LookupRating ?? "null"}, Work Key:{LookupWorkKey ?? "null"}");
                Console.WriteLine();

                var results = await this._dataService.FetchResults(LookupRating, LookupWorkKey);

                if (results != null && results.Count > 0)
                {
                    foreach (var result in results)
                    {
                        Console.Write($"{result.FetchType}: ");
                        
                        if (result.Success)
                        {
                            Console.Write($"Output saved to {result.FileName}");

                            if (result.Message != null)
                                Console.WriteLine(result.Message);
                        }
                        else
                        {
                            Console.Write(result.Message);
                            Console.Write(" Fetch was not successful");
                            Console.WriteLine();
                        }

                        Console.WriteLine();
                    }
                }
                else
                { 
                    Console.WriteLine("No results after fetch"); 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Unhandled Exception while fetching data results:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

    }
}
