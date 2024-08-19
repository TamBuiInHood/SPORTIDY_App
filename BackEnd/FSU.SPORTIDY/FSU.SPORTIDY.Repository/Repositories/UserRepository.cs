using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(SportidyContext context) : base(context)
        {
        }

        public async Task AddUserAsync(User newUser)
        {
            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
           return await context.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));  
        }
    }
}
