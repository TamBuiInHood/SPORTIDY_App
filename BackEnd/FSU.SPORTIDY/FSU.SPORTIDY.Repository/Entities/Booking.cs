using System;
using System.Collections.Generic;

namespace FSU.SPORTIDY.Repository.Entities;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? BookingCode { get; set; }

    public DateTime? BookingDate { get; set; }

    public int? Price { get; set; }

    public DateTime? DateStart { get; set; }

    public DateTime? DateEnd { get; set; }

    public int? Status { get; set; }

    public int? PaymentMethod { get; set; }

    public int? BarCode { get; set; }

    public int PlayFieldId { get; set; }

    public string? Description { get; set; }

    public int? CustomerId { get; set; }

    public virtual PlayField PlayField { get; set; } = null!;

    public virtual ICollection<PlayFieldFeedback> PlayFieldFeedbacks { get; set; } = new List<PlayFieldFeedback>();
}
