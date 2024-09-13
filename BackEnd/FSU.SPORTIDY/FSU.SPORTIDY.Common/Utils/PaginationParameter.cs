using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Common.Utils
{
    public class PaginationParameter
    {
        [FromQuery(Name = "page-index")]
        public int PageIndex { get; set; }
        [FromQuery(Name = "page-size")]
        public int PageSize { get; set; }
        [FromQuery(Name = "search-key")]
        public string? Search {  get; set; }
        [FromQuery(Name = "sort-by")]
        public string? SortBy { get; set; }
        [FromQuery(Name = "direction")]
        public string? Direction { get; set; }

    }
}
