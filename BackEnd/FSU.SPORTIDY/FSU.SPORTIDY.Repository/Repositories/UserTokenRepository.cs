using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Repository.Repositories
{
    public class UserTokenRepository : GenericRepository<UserToken>, IUserTokenRepository
    {
        public UserTokenRepository(SportidyContext context) : base(context)
        {
        }

        public async Task AddUserToken(UserToken userToken)
        {
            await context.UserTokens.AddAsync(userToken);
            await context.SaveChangesAsync();
        }
    }
}
