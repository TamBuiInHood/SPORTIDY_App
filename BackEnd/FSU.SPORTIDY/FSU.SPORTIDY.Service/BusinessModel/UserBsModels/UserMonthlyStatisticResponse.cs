using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.UserBsModels
{
    public class UserMonthlyStatisticResponse
    {
        public int TotalUsers { get; set; }          
        public List<UserMonthlyStatistic> MonthlyStatistics { get; set; } 
    }
}
