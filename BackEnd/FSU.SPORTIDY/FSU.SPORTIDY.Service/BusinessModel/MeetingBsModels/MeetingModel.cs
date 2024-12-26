using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Service.BusinessModel.ClubModels;
using FSU.SPORTIDY.Service.BusinessModel.MeetingBsModels;

namespace FSU.SPORTIDY.Service.BusinessModel.MeetingModels
{
    public class MeetingModel
    {
        public int MeetingId { get; set; }

        public string? MeetingCode { get; set; }

        public string? MeetingName { get; set; }

        public string? MeetingImage { get; set; }

        public string? Address { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? Host { get; set; }

        public int? TotalMember { get; set; }

        public int? ClubId { get; set; }

        public string? Note { get; set; }

        public bool? IsPublic { get; set; }

        public int? SportId { get; set; }

        public int? CancelBefore { get; set; }
        public string? ClubName { get; set; }
        public string? ImageClub { get; set; }

        public virtual ICollection<CommentInMeetingModel> CommentInMeetings { get; set; } = new List<CommentInMeetingModel>();

        public virtual ICollection<UserMeeting> UserMeetings { get; set; } = new List<UserMeeting>();

    }
}
