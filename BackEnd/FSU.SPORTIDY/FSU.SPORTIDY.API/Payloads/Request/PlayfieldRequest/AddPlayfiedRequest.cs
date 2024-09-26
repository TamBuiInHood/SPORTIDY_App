using FSU.SPORTIDY.Repository.Entities;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using System.ComponentModel.DataAnnotations;

namespace FSU.SPORTIDY.API.Payloads.Request.PlayfieldRequest
{
    public class AddPlayfiedRequest
    {
        [Required]
        public int? CurrentIDLogin { get; set; }
        [Required]
        public string? PlayFieldName { get; set; }
        [Required]
        public int? Price { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public DateTime? OpenTime { get; set; }
        [Required]
        public DateTime? CloseTime { get; set; }

        [Required]
        public List<string> subPlayfieds { get; set; } = new List<string>();
        [Required]
        public int sportId { get; set; }
        [Required]
        [FromForm]
        [FileFormat(".jpg", ".jpeg", ".png")]
        public IFormFile? AvatarImage { get; set; }
        [FromForm]
        public List<AddImageFieldRequest> AddImageField { get; set; } = new List<AddImageFieldRequest>();

    }
}
