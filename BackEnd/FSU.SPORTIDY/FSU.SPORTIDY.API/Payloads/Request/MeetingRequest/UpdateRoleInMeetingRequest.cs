using Newtonsoft.Json;

namespace FSU.SPORTIDY.API.Payloads.Request.MeetingRequest
{

    public class UpdateRoleInMeetingRequest
    {
        public int userId { get; set; }
        public int meetingId { get; set; }
        public string roleInMeeting { get; set; }
    }

    public class KickUserRequest
    {
        public int userId { get; set; }
        public int meetingId { get; set; }
        public string roleInMeeting { get; set; }
    }

    public class InsertUserMeetingRequest
    {
        public int userId { get; set; }
        public int meetingId { get; set; }
    }

}
