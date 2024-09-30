using FSU.SPORTIDY.Common.Status;
using System.ComponentModel.DataAnnotations;

namespace FSU.SPORTIDY.API.Payloads.Request.BookingRequest
{
    public class UpdateBookingStatusRequest
    {
        [Required]
        public int bookingId {  get; set; }
        [Required] 
        public BookingStatusEnum status { get; set; }
    }
}
