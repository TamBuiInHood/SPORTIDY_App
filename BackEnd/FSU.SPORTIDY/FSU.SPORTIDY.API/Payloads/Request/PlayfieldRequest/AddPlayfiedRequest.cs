using FSU.SPORTIDY.Repository.Entities;
using Microsoft.AspNetCore.Mvc;
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
        public List<string> subPlayfieds = new List<string>();
        [Required]
        [FromForm]
        [FileFormat(".jpg", ".jpeg", ".png")]
        public IFormFile? AvatarImage { get; set; }
        [FromForm]
        public  List<AddImageFieldRequest> AddImageField { get; set; } = new List<AddImageFieldRequest>();

    }
}
