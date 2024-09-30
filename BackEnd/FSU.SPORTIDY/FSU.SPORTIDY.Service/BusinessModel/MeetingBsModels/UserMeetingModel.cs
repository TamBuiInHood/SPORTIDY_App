using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Service.BusinessModel.MeetingModels;
using FSU.SPORTIDY.Service.BusinessModel.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.MeetingBsModels
{
    public class UserMeetingModel
    {
        public int? ClubId { get; set; }

        public string? RoleInMeeting { get; set; }

        public int UserId { get; set; }

        public int MeetingId { get; set; }

        public virtual MeetingModel Meeting { get; set; } = null!;

        public virtual UserModel User { get; set; } = null!;
    }
}
