using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.BookingBsModels
{
    public class PlayFieldRevenueResponse
    {
        public List<MonthlyRevenue> MonthlyRevenues { get; set; }
        public double? TotalRevenue { get; set; } 
    }
}
