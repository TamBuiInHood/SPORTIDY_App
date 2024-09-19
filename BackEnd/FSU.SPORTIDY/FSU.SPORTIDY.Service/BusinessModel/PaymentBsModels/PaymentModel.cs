using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Service.BusinessModel.BookingBsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.PaymentBsModels
{
    public class PaymentModel
    {
        public int PaymentId { get; set; }
        public string? OrderCode { get; set; }
        public double? Amount { get; set; }
        public DateTime? DateOfTransaction { get; set; }
        public int? Status { get; set; }
        public int? BookingId { get; set; }

        public virtual BookingModel? Booking { get; set; }
    }
}
