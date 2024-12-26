using System;
using System.Collections.Generic;

namespace FSU.SPORTIDY.Repository.Entities;

public partial class Sport
{
    public int SportId { get; set; }

    public string? SportCode { get; set; }

    public string? SportName { get; set; }

    public string? SportImage { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
    public virtual ICollection<PlayField> PlayFields { get; set; } = new List<PlayField>();
}
