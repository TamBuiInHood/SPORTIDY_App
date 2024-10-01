using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FSU.SPORTIDY.API.Payloads.Request.MeetingRequest
{
    public class AddMeetingRequest
    {
        [Required]
        public string? meetingName { get; set; }
        public IFormFile? meetingImage { get; set; }
        [Required]
        public string? address { get; set; }
        [Required]
        public DateTime? startDate { get; set; }
        [Required]
        public DateTime? endDate { get; set; }
        [Required]
        public int? totalMember { get; set; }
        public int? clubId { get; set; }
        public string? note { get; set; }
        [Required]
        public bool? isPublic { get; set; }
        [Required]
        public int? sportId { get; set; }
        [Required]
        public int? cancelBefore { get; set; }
        //public List<int> InvitedFriend { get; set; }
        [Required]
        public int currentIdLogin { get; set; }
    }
}
