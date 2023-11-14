using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eComm.Interview.Data.Models.OpenLibrary
{
    public class Created
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("value")]
        public DateTime? Value { get; set; }
    }

    public class LastModified
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("value")]
        public DateTime? Value { get; set; }
    }

    public class Type
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }

    public class Author
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }

    public class Language
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }

    public class Identifiers
    {
        [JsonPropertyName("goodreads")]
        public List<string> Goodreads { get; set; }

        [JsonPropertyName("librarything")]
        public List<string> Librarything { get; set; }
    }

    public class AuthorData
    {
        [JsonPropertyName("author")]
        public Author2 Author { get; set; }

        //[JsonPropertyName("type")]
        //public string Type { get; set; }
       
        //public Type Type { get; set; }
    }

    public class Author2
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }

    public class Link
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("type")]
        public Type Type { get; set; }
    }

    public class RemoteIds
    {
        [JsonPropertyName("viaf")]
        public string Viaf { get; set; }

        [JsonPropertyName("wikidata")]
        public string Wikidata { get; set; }

        [JsonPropertyName("isni")]
        public string Isni { get; set; }
    }

    public class FirstSentence
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }

}
