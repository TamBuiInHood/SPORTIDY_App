﻿using System;
using System.Collections.Generic;

namespace FSU.SPORTIDY.Repository.Entities;

public partial class PlayField
{
    public int PlayFieldId { get; set; }

    public string? PlayFieldCode { get; set; }

    public string? PlayFieldName { get; set; }

    public double? Price { get; set; }

    public string? Address { get; set; }

    public DateTime? OpenTime { get; set; }

    public int? UserId { get; set; }

    public DateTime? CloseTime { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<ImageField> ImageFields { get; set; } = new List<ImageField>();

    public virtual User? User { get; set; }
}
