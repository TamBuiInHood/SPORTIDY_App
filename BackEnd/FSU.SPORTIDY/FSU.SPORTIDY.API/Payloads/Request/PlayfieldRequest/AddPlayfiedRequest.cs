using FSU.SPORTIDY.Repository.Entities;
using System.ComponentModel.DataAnnotations;

namespace FSU.SPORTIDY.API.Payloads.Request.PlayfieldRequest
{
    public class AddPlayfiedRequest
    {
        [Required]
        public string? PlayFieldName { get; set; }
        [Required]
        public int? Price { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public TimeOnly? OpenTime { get; set; }
        [Required]
        public TimeOnly? CloseTime { get; set; }
        [Required]
        [FileFormat(".jpg", ".jpeg", ".png")]
        public IFormFile? AvatarImage { get; set; }

        public  List<AddImageFieldRequest> ImageFields { get; set; } = new List<AddImageFieldRequest>();

    }
}
