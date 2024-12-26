using FSU.SPORTIDY.Common.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.PaymentBsModels
{
    public class PaymentStatistic
    {
        public string? Email { get; set; }
        public DateTime? DateOfTransaction { get; set; }
        public double? TotalAmount { get; set; }
        public PaymentStatus? Status { get; set; }
    }
}
