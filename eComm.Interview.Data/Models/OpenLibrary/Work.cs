using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eComm.Interview.Data.Models.OpenLibrary
{
    public class Work
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("covers")]
        public List<int?> Covers { get; set; }

        [JsonPropertyName("authors")]
        public List<AuthorData> Authors { get; set; }

        [JsonPropertyName("subjects")]
        public List<string> Subjects { get; set; }

        //[JsonPropertyName("type")]
        //public Type Type { get; set; }

        //[JsonPropertyName("description")]
        //public string Description { get; set; }

        //[JsonPropertyName("subject_places")]
        //public List<string> SubjectPlaces { get; set; }

        //[JsonPropertyName("subject_people")]
        //public List<string> SubjectPeople { get; set; }

        //[JsonPropertyName("subject_times")]
        //public List<string> SubjectTimes { get; set; }

        //[JsonPropertyName("location")]
        //public string Location { get; set; }

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
