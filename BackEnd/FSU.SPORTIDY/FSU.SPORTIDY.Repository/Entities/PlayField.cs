using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FSU.SPORTIDY.Repository.Entities;

public partial class PlayField
{
    public int PlayFieldId { get; set; }

    public string? PlayFieldCode { get; set; }

    public string? PlayFieldName { get; set; }

    public int? Price { get; set; }

    public string? Address { get; set; }

    public TimeOnly? OpenTime { get; set; }

    public int? UserId { get; set; }

    public TimeOnly? CloseTime { get; set; }

    public string? AvatarImage { get; set; }

    public int Status { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<ImageField> ImageFields { get; set; } = new List<ImageField>();

    public virtual User? User { get; set; }
}
