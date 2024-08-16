using System;
using System.Collections.Generic;

namespace FSU.SPORTIDY.Repository.Entities;

public partial class Sport
{
    public int SportId { get; set; }

    public int? SportCode { get; set; }

    public int? SportName { get; set; }

    public int? SportIamge { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
