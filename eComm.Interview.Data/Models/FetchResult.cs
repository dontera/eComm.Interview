using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static eComm.Interview.Data.Helpers.Enums;

namespace eComm.Interview.Data.Models
{
    public class FetchResult
    {
        public FetchType FetchType {  get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? FileName { get; set; }
    }
}
