using FSU.SPORTIDY.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Repository.Interfaces
{
    public interface IUserTokenRepository
    {
        public Task AddUserToken(UserToken userToken);
        public Task<UserToken> GetUserTokenByRefreshToken(string refreshToken);
        public Task<bool> UpdateToken(UserToken userToken);
        public Task<bool> DeleteToken(string deleteRefreshToken);
    }
}
