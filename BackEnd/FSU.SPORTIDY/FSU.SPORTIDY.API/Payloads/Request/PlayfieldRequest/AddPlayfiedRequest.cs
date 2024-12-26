using FSU.SPORTIDY.Repository.Entities;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using System.ComponentModel.DataAnnotations;

namespace FSU.SPORTIDY.API.Payloads.Request.PlayfieldRequest
{
    public class AddPlayfiedRequest
    {
        public int? currentIdLogin { get; set; }
        public string? playfieldName { get; set; }
        public int? price { get; set; }
        [Required]
        public string? address { get; set; }
        [Required]
        public DateTime? openTime { get; set; }
        [Required]
        public DateTime? closeTime { get; set; }

        [Required]
        public List<string> subPlayfieds { get; set; } = new List<string>();
        [Required]
        public int sportId { get; set; }
        [FromForm]
        [FileFormat(".jpg", ".jpeg", ".png")]
        public IFormFile? avatarImage { get; set; }
        [FromForm]
        public List<AddImageFieldRequest> addImageField { get; set; } = new List<AddImageFieldRequest>();

    }
}
