using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FSU.SPORTIDY.API.Payloads.Request.BookingRequest
{
    public class UpdateBookingRequest
    {
        [Required]
        public int bookingId { get; set; }
        public DateTime? dateStart { get; set; }

        public DateTime? dateEnd { get; set; }

        [FileExtensions(Extensions = "jpg,jpeg,png,pdf", ErrorMessage = "Please upload a valid file format (.jpg, .jpeg, .png, .pdf).")]
        public IFormFile? barCode { get; set; }
        [MaxLength(500, ErrorMessage = "Description can be at most 500 characters long.")]
        public string? description { get; set; }

        [Required(ErrorMessage = "CustomerId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "CustomerId must be a positive number.")]
        public int? customerId { get; set; }

    }
}
