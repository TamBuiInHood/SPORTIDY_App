using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Repository.Utils
{
    public class PaginationParameter
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        [FromQuery(Name = "search-key")]
        public string? Search {  get; set; }
        [FromQuery(Name = "sort-by")]
        public string? SortBy { get; set; }
        public string? Direction { get; set; }

    }
}
