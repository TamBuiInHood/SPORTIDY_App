using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.BookingBsModels
{
    public class PlayFieldRevenueForAdmin
    {
        public int Year { get; set; }
        public List<MonthlyRevenue> MonthlyRevenues { get; set; }
        public double? TotalRevenue { get; set; }
    }
}
