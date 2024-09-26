using System;
using System.Collections.Generic;

namespace FSU.SPORTIDY.Repository.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string? UserCode { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }
    public string? FullName { get; set; }

    public string? Address { get; set; }

    public string? Otp { get; set; }

    public string? Email { get; set; }

    public int? Gender { get; set; }

    public string? Description { get; set; }

    public string? Avartar { get; set; }

    public DateOnly? Birtday { get; set; }

    public int RoleId { get; set; }

    public DateOnly? CreateDate { get; set; }

    public int? Status { get; set; }

    public int? IsDeleted { get; set; }

    public string? Phone { get; set; }
    public string? BankCode { get; set; }
    public string? BankName { get; set; }
    public string? DeviceCode { get; set; }

    public virtual ICollection<Friendship> FriendshipUserId1Navigations { get; set; } = new List<Friendship>();

    public virtual ICollection<Friendship> FriendshipUserId2Navigations { get; set; } = new List<Friendship>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<PlayField> PlayFields { get; set; } = new List<PlayField>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<UserClub> UserClubs { get; set; } = new List<UserClub>();

    public virtual ICollection<UserMeeting> UserMeetings { get; set; } = new List<UserMeeting>();

    public virtual ICollection<Sport> Sports { get; set; } = new List<Sport>();
    public virtual ICollection<SystemFeedback> SystemFeedbacks { get; set; } = new List<SystemFeedback>();
}
