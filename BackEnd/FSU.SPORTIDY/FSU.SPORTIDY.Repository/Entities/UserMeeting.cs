using System;
using System.Collections.Generic;

namespace FSU.SPORTIDY.Repository.Entities;

public partial class UserMeeting
{
    public int? ClubId { get; set; }

    public string? RoleInMeeting { get; set; }

    public int UserId { get; set; }

    public int MeetingId { get; set; }

    public virtual Meeting Meeting { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
