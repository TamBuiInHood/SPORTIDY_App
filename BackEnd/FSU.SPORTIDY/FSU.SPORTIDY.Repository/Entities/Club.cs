using System;
using System.Collections.Generic;

namespace FSU.SPORTIDY.Repository.Entities;

public partial class Club
{
    public int ClubId { get; set; }

    public string? ClubCode { get; set; }

    public string? ClubName { get; set; }

    public string? Regulation { get; set; }

    public string? Infomation { get; set; }

    public string? Slogan { get; set; }

    public string? MainSport { get; set; }

    public DateOnly? CreateDate { get; set; }

    public string? Location { get; set; }

    public int? TotalMember { get; set; }

    public string? AvartarClub { get; set; }

    public string? CoverImageClub { get; set; }

    public virtual ICollection<UserClub> UserClubs { get; set; } = new List<UserClub>();
}
