using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Service.BusinessModel.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.FriendShipBSModels
{
    public class FriendShipModel
    {
        public int FriendShipId { get; set; }

        public string? FriendShipCode { get; set; }

        public int? Status { get; set; }

        public DateOnly? CreateDate { get; set; }

        public int UserId1 { get; set; }

        public int UserId2 { get; set; }

        public int RequestBy {  get; set; }

        public virtual UserModel UserId1Navigation { get; set; } = null!;

        public virtual UserModel UserId2Navigation { get; set; } = null!;
    }
}
