using System;
using System.Collections.Generic;

namespace FSU.SPORTIDY.Repository.Entities;

public partial class Meeting
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

    public int? Status { get; set; }

    public virtual ICollection<CommentInMeeting> CommentInMeetings { get; set; } = new List<CommentInMeeting>();

    public virtual ICollection<UserMeeting> UserMeetings { get; set; } = new List<UserMeeting>();
}
