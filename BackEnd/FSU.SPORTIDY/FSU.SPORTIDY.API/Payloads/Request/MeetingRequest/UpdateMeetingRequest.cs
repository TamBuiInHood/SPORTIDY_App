using System.ComponentModel.DataAnnotations;

namespace FSU.SPORTIDY.API.Payloads.Request.MeetingRequest
{
    public class UpdateMeetingRequest
    {
        public string? MeetingName { get; set; }
        public string? MeetingImage { get; set; }
        public string? Address { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? TotalMember { get; set; }
        public string? Note { get; set; }
        public bool? IsPublic { get; set; }
        public int? CancelBefore { get; set; }
    }
}
