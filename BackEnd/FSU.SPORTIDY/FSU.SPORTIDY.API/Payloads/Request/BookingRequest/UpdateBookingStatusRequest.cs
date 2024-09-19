using FSU.SPORTIDY.Common.Status;
using System.ComponentModel.DataAnnotations;

namespace FSU.SPORTIDY.API.Payloads.Request.BookingRequest
{
    public class UpdateBookingStatusRequest
    {
        [Required]
        public int BookingId {  get; set; }
        [Required] 
        public BookingStatusEnum Status { get; set; }
    }
}
