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
        [FromQuery(Name = "pageIndex")]
        public int PageIndex { get; set; }
        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; }
        [FromQuery(Name = "searchKey")]
        public string? Search {  get; set; }
        [FromQuery(Name = "sortBy")]
        public string? SortBy { get; set; }
        [FromQuery(Name = "direction")]
        public string? Direction { get; set; }

    }
}
