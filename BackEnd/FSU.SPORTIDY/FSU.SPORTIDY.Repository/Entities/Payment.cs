using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Repository.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public string? OrderCode { get; set; }
        public double? Amount { get; set; }
        public DateTime? DateOfTransaction { get; set; }
        public int? Status { get; set; }
        public int? BookingId { get; set;}

        public virtual Booking? Booking { get; set; }
    }
}
