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

    public double? Price { get; set; }

    public string? Address { get; set; }

    public TimeOnly? OpenTime { get; set; }

    public int? UserId { get; set; }

    public TimeOnly? CloseTime { get; set; }

    public string? AvatarImage { get; set; }

    public int Status { get; set; }

    public int? IsDependency { get; set; }

    public int? SportId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<ImageField> ImageFields { get; set; } = new List<ImageField>();

    public virtual User? User { get; set; }

    public virtual PlayField? PlayFieldContainer { get; set; }
    public virtual ICollection<PlayField>? ListSubPlayFields { get; set; }
    public virtual Sport? Sport { get; set; }


}
