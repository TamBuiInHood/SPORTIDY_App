using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Repository.Entities
{
    public class UserToken
    {
        public int UserTokenId { get; set; }
        public int UserId { get; set; }
        public string AccessToken { get; set; } = "";
        public string ExpiredTimeAccessToken { get; set; } = "";
        public string RefreshToken { get; set; } = "";
        public string ExpiredTimeRefreshToken { get; set; } = "";

        public DateTime CreateDate { get; set; }
    }
}
