using Azure.Core;
using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Repository.Repositories
{
    public class UserTokenRepository : GenericRepository<UserToken>, IUserTokenRepository
    {
        private readonly SportidyContext _context;
        public UserTokenRepository(SportidyContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddUserToken(UserToken userToken)
        {
            await _context.UserTokens.AddAsync(userToken);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteToken(string deleteRefreshToken)
        {
            UserToken deleteToken = await context.UserTokens.FirstOrDefaultAsync(x => x.RefreshToken == deleteRefreshToken);
            if (deleteToken != null)
            {
                context.UserTokens.Remove(deleteToken);
                var result = await context.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }

        public async Task<UserToken> GetUserTokenByRefreshToken(string refreshToken)
        {
            var result = await context.UserTokens.FirstOrDefaultAsync(x => x.RefreshToken.Equals(refreshToken));
            return result;
        }

        public async Task<bool> UpdateToken(UserToken userToken)
        {
            var oldUserToken = await context.UserTokens.FirstOrDefaultAsync(x => x.UserId == userToken.UserId);
            if (oldUserToken != null)
            {
                oldUserToken.AccessToken = userToken.AccessToken;
                oldUserToken.ExpiredTimeAccessToken = userToken.ExpiredTimeAccessToken;
                oldUserToken.RefreshToken = userToken.RefreshToken;
                oldUserToken.ExpiredTimeRefreshToken = userToken.ExpiredTimeRefreshToken;
            }
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
    }
}
