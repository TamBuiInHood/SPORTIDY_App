using System.ComponentModel.DataAnnotations;

namespace FSU.SPORTIDY.API.Payloads.Request.BookingRequest
{
    public class AddBookingRequest
    {
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "DateStart is required.")]
        public DateTime? DateStart { get; set; }

        [Required(ErrorMessage = "DateEnd is required.")]
        public DateTime? DateEnd { get; set; }

        [Required(ErrorMessage = "BarCode file is required.")]
        [FileExtensions(Extensions = "jpg,jpeg,png,pdf", ErrorMessage = "Please upload a valid file format (.jpg, .jpeg, .png, .pdf).")]
        public IFormFile? BarCode { get; set; }

        [Required(ErrorMessage = "PlayFieldId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "PlayFieldId must be a positive number.")]
        public int PlayFieldId { get; set; }

        [MaxLength(500, ErrorMessage = "Description can be at most 500 characters long.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "CustomerId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "CustomerId must be a positive number.")]
        public int? CustomerId { get; set; }
    }

}
