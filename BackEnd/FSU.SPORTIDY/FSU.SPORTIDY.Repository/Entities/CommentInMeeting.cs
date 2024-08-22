using System;
using System.Collections.Generic;

namespace FSU.SPORTIDY.Repository.Entities;

public partial class CommentInMeeting
{
    public int CommentId { get; set; }

    public string? CommentCode { get; set; }

    public DateTime? CommentDate { get; set; }

    public int UserId { get; set; }

    public string? Content { get; set; }

    public string? Image { get; set; }

    public int MeetingId { get; set; }

    public virtual Meeting Meeting { get; set; } = null!;
}
