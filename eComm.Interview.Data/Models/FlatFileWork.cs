using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eComm.Interview.Data.Models
{
    public class FlatFileWork
    {
        public required string WorkKey {  get; set; }
        public string? EditionKey { get; set; }
        public required int Rating { get; set; }
        public required DateTime Date {  get; set; }
    }
}
