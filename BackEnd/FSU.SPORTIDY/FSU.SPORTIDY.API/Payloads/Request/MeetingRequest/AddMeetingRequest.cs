using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FSU.SPORTIDY.API.Payloads.Request.MeetingRequest
{
    public class AddMeetingRequest
    {
        [Required]
        public string? MeetingName { get; set; }
        public string? MeetingImage { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
        [Required]
        public int? TotalMember { get; set; }
        [Required]
        public int? ClubId { get; set; }
        public string? Note { get; set; }
        [Required]
        public bool? IsPublic { get; set; }
        [Required]
        public int? SportId { get; set; }
        [Required]
        public int? CancelBefore { get; set; }
        public List<int> InvitedFriend { get; set; }
        [Required]
        public int currentIDLogin { get; set; }
    }
}
