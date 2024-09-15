using System;
using System.Collections.Generic;

namespace FSU.SPORTIDY.Repository.Entities;

public partial class Friendship
{
    public int FriendShipId { get; set; }

    public string? FriendShipCode { get; set; }

    public int? Status { get; set; }

    public DateOnly? CreateDate { get; set; }

    public int UserId1 { get; set; }

    public int UserId2 { get; set; }

    public int RequestBy { get; set; }
    public virtual User UserId1Navigation { get; set; } = null!;

    public virtual User UserId2Navigation { get; set; } = null!;
}
