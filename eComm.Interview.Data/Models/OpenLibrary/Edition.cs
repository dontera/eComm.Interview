using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eComm.Interview.Data.Models.OpenLibrary
{
    public class Edition
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("authors")]
        public List<Author> Authors { get; set; }

        [JsonPropertyName("covers")]
        public List<int?> Covers { get; set; }

        [JsonPropertyName("subjects")]
        public List<string> Subjects { get; set; }

        //[JsonPropertyName("identifiers")]
        //public Identifiers Identifiers { get; set; }

        //[JsonPropertyName("publish_date")]
        //public string PublishDate { get; set; }

        //[JsonPropertyName("publishers")]
        //public List<string> Publishers { get; set; }

        //[JsonPropertyName("contributions")]
        //public List<string> Contributions { get; set; }

        //[JsonPropertyName("languages")]
        //public List<Language> Languages { get; set; }

        //[JsonPropertyName("source_records")]
        //public List<string> SourceRecords { get; set; }

        //[JsonPropertyName("local_id")]
        //public List<string> LocalId { get; set; }

        //[JsonPropertyName("type")]
        //public Type Type { get; set; }

        //[JsonPropertyName("first_sentence")]
        //public FirstSentence FirstSentence { get; set; }

        //[JsonPropertyName("number_of_pages")]
        //public int? NumberOfPages { get; set; }

        //[JsonPropertyName("works")]
        //public List<Work> Works { get; set; }

        //[JsonPropertyName("ocaid")]
        //public string Ocaid { get; set; }

        //[JsonPropertyName("isbn_10")]
        //public List<string> Isbn10 { get; set; }

        //[JsonPropertyName("isbn_13")]
        //public List<string> Isbn13 { get; set; }

        //[JsonPropertyName("latest_revision")]
        //public int? LatestRevision { get; set; }

        //[JsonPropertyName("revision")]
        //public int? Revision { get; set; }

        //[JsonPropertyName("created")]
        //public Created Created { get; set; }

        //[JsonPropertyName("last_modified")]
        //public LastModified LastModified { get; set; }
    }

}
