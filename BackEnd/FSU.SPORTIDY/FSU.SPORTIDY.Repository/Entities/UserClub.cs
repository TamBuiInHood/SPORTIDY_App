using System;
using System.Collections.Generic;

namespace FSU.SPORTIDY.Repository.Entities;

public partial class UserClub
{
    public bool? IsLeader { get; set; }

    public int UserId { get; set; }

    public int ClubId { get; set; }

    public virtual Club Club { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
