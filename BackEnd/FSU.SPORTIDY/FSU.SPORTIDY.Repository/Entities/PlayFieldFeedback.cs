using System;
using System.Collections.Generic;

namespace FSU.SPORTIDY.Repository.Entities;

public partial class PlayFieldFeedback
{
    public int FeedbackId { get; set; }

    public string? FeedbackCode { get; set; }

    public string? Content { get; set; }

    public int? Rating { get; set; }

    public int? FeedbackDate { get; set; }

    public int? ImageUrl { get; set; }

    public int? VideoUrl { get; set; }

    public bool? IsAnonymous { get; set; }

    public int BookingId { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
