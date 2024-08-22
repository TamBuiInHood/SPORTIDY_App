using System;
using System.Collections.Generic;

namespace FSU.SPORTIDY.Repository.Entities;

public partial class Notification
{
    public int NotificationId { get; set; }

    public string? NotificationCode { get; set; }

    public string? Tiltle { get; set; }

    public string? Message { get; set; }

    public bool? NotificationType { get; set; }

    public bool? IsAccept { get; set; }

    public DateTime? InviteDate { get; set; }

    public bool? IsRead { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
