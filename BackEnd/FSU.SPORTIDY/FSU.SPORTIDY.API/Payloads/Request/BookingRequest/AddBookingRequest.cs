using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FSU.SPORTIDY.API.Payloads.Request.BookingRequest
{
    public class AddBookingRequest
    {
        //[Required]
        //public string bookingCode { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public double? price { get; set; }

        [Required(ErrorMessage = "DateStart is required.")]
        public DateTime? dateStart { get; set; }

        [Required(ErrorMessage = "DateEnd is required.")]
        public DateTime? dateEnd { get; set; }

        //[FileFormat(".jpg", ".jpeg", ".png", ".pdf", ErrorMessage = "Please upload a valid file format (.jpg, .jpeg, .png, .pdf).")]
        //public IFormFile? barCode { get; set; }

        [Required(ErrorMessage = "PlayFieldId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "PlayFieldId must be a positive number.")]
        public int playFieldId { get; set; }

        [MaxLength(500, ErrorMessage = "Description can be at most 500 characters long.")]
        public string? description { get; set; }

        [Required(ErrorMessage = "CustomerId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "CustomerId must be a positive number.")]
        public int? customerId { get; set; }


        [MaxLength(500, ErrorMessage = "Voucher can be at most 500 characters long.")]
        public string? voucher { get; set; }
    }

}
