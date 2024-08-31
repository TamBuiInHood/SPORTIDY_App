using System;
using System.Collections.Generic;

namespace FSU.SPORTIDY.Repository.Entities;

public partial class ImageField
{
    public int ImageId { get; set; }

    public string? ImageUrl { get; set; }

    public string? VideoUrl { get; set; }

    public int? ImageIndex { get; set; }

    public int? PlayFieldId { get; set; }

    public virtual PlayField? PlayField { get; set; }
}
