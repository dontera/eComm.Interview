using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eComm.Interview.Data.Models.OpenLibrary
{    
    public class AuthorRecord
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        //[JsonPropertyName("bio")]
        //public string Bio { get; set; }

        //[JsonPropertyName("remote_ids")]
        //public RemoteIds RemoteIds { get; set; }

        //[JsonPropertyName("photos")]
        //public List<int?> Photos { get; set; }

        //[JsonPropertyName("alternate_names")]
        //public List<string> AlternateNames { get; set; }

        //[JsonPropertyName("personal_name")]
        //public string PersonalName { get; set; }

        //[JsonPropertyName("links")]
        //public List<Link> Links { get; set; }

        //[JsonPropertyName("source_records")]
        //public List<string> SourceRecords { get; set; }

        //[JsonPropertyName("type")]
        //public Type Type { get; set; }

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
