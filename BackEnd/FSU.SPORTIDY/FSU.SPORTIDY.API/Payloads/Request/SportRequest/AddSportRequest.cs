using System.ComponentModel.DataAnnotations;

namespace FSU.SPORTIDY.API.Payloads.Request.SportRequest
{
    public class AddSportRequest
    {
        [Required]
        public string? sportName { get; set; }
        [Required]
        public IFormFile? sportImage { get; set; }
    }
}
