using FSU.SPORTIDY.Service.BusinessModel.MeetingModels;

namespace FSU.SPORTIDY.Service.BusinessModel.MeetingBsModels
{
    public class CommentInMeetingModel
    {
        public int CommentId { get; set; }

        public string? CommentCode { get; set; }

        public DateTime? CommentDate { get; set; }

        public int UserId { get; set; }

        public string? Content { get; set; }

        public string? Image { get; set; }

        public int MeetingId { get; set; }

        public virtual MeetingModel Meeting { get; set; } = null!;
    }
}
