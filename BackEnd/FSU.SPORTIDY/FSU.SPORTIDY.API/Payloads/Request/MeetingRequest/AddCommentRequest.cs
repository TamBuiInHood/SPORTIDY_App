namespace FSU.SPORTIDY.API.Payloads.Request.MeetingRequest
{
    public class AddCommentRequest
    {
        public int userId { get; set; }

        public string? content { get; set; }

        public IFormFile? image { get; set; }

        public int meetingId { get; set; }
    }
}
