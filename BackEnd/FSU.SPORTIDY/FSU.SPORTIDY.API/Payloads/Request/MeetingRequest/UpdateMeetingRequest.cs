using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FSU.SPORTIDY.API.Payloads.Request.MeetingRequest
{
    public class UpdateMeetingRequest
    {
        public string? meetingName { get; set; }
        public string? meetingImage { get; set; }
        public string? address { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int? totalMember { get; set; }
        public string? note { get; set; }
        public bool? isPublic { get; set; }
        public int? cancelBefore { get; set; }
    }
}
