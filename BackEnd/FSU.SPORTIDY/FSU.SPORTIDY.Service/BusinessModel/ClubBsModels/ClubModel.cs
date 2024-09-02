using FSU.SPORTIDY.Service.BusinessModel.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.ClubModels
{
    public class ClubModel
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

        public string? CoverImageClub { get; set; } = null;

        public List<UserModel> ListMember { get; set; } = null;


    }
}
