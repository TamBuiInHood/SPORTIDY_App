using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.ClubModels
{
    public class UpdateClubModel
    {
        public int clubId { get; set; }
        public string? clubName { get; set; }

        public string? regulation { get; set; }

        public string? infomation { get; set; }

        public string? slogan { get; set; }

        public string? mainSport { get; set; }

        public string? location { get; set; }

        public int? totalMember { get; set; }

        public string? avartarClub { get; set; }

        public string? coverImageClub { get; set; } = null;
    }
}
