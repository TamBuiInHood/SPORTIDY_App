using System.ComponentModel.DataAnnotations;

namespace FSU.SPORTIDY.API.Payloads.Request.BookingRequest
{
    public class UpdateBookingRequest
    {
        [Required]
        public int BookingId { get; set; }
        public DateTime? DateStart { get; set; }

        public DateTime? DateEnd { get; set; }

        [FileExtensions(Extensions = "jpg,jpeg,png,pdf", ErrorMessage = "Please upload a valid file format (.jpg, .jpeg, .png, .pdf).")]
        public IFormFile? BarCode { get; set; }
        [MaxLength(500, ErrorMessage = "Description can be at most 500 characters long.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "CustomerId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "CustomerId must be a positive number.")]
        public int? CustomerId { get; set; }

    }
}
