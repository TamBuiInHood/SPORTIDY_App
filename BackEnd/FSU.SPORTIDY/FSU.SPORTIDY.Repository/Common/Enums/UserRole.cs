using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Repository.Common.Enums
{
    public class UserRole
    {
        private readonly IConfiguration _config;

        public static String Admin = "";
        public static String OwnerPlayfield = "";
        public static String Player = "";
        public UserRole(IConfiguration config)
        {
            _config = config;
            Admin = _config["UserRoles:Admin"]!;
            OwnerPlayfield = _config["UserRoles:OwnerPlayfield"]!;
            Player = _config["UserRoles:Player"]!;
        }

    }
}
