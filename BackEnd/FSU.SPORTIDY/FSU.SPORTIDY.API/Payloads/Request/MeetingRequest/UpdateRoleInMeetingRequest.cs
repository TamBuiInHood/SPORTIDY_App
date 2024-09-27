namespace FSU.SPORTIDY.API.Payloads.Request.MeetingRequest
{

    public class UpdateRoleInMeetingRequest
    {
        public int UserId { get; set; }
        public int MeetingId { get; set; }
        public string RoleInMeeting { get; set; }
    }

    public class KickUserRequest
    {
        public int UserId { get; set; }
        public int MeetingId { get; set; }
        public string RoleInMeeting { get; set; }
    }

    public class InsertUserMeetingRequest
    {
        public int UserId { get; set; }
        public int MeetingId { get; set; }
        public int? ClubId { get; set; }
    }

}
