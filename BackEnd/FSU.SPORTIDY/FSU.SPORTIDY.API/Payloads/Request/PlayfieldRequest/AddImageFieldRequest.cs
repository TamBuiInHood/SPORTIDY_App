using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FSU.SPORTIDY.API.Payloads.Request.PlayfieldRequest
{
    //[AtLeastOneRequired(nameof(ImageUrl), nameof(VideoUrl))]
    public class AddImageFieldRequest
    {
        [FromForm]
        [FileFormat(".jpg", ".jpeg", ".png")]
        public IFormFile ImageUrl { get; set; }
        [Required]
        public int? ImageIndex { get; set; }
    }
}
