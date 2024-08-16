using System;
using System.Collections.Generic;

namespace FSU.SPORTIDY.Repository.Entities;

public partial class ImageField
{
    public int ImageId { get; set; }

    public string? ImageUrl { get; set; }

    public int? VideoUrl { get; set; }

    public bool? IsSportlight { get; set; }

    public int? PlayFieldId { get; set; }

    public virtual PlayField? PlayField { get; set; }
}
