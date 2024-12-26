using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.BusinessModel.UserModels
{
    public class UserModel
    {
        public int UserId { get; set; }

        public string? UserCode { get; set; }

        public string? UserName { get; set; }

        public string? FullName { get; set; }

        public string? Address { get; set; }

        public string? Email { get; set; }

        public int? Gender { get; set; }

        public string? Description { get; set; }

        public string? Avartar { get; set; }

        public DateOnly? Birtday { get; set; }

        public string? RoleName { get; set; }

        public DateOnly? CreateDate { get; set; }

        public int? Status { get; set; }

        public int? IsDeleted { get; set; }

        public string? Phone { get; set; }
        public bool? IsLeader { get; set; }
        public int RoleId { get; set; }
    }
}
