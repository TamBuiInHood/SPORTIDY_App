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
        private readonly SportidyContext _context;

        public UserRepository(SportidyContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User newUser)
        {
            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsersByRole(string roleName)
        {
            var result = await _context.Users.Include(x => x.Role)
                                        .Where(x => x.Role.RoleName.ToLower() == roleName.ToLower())
                                        .ToListAsync();    
            return result;

        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
           return await context.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));  
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
           var user = await context.Users.FirstOrDefaultAsync(x =>x.UserId == userId);  
            if(user != null)
            {
                return user;    
            }
            return null;
        }

        public async Task<List<User>> GetUsersByYear(int year)
        {
            var users = await _context.Users
                            .Where(u => u.CreateDate.HasValue)  
                            .ToListAsync();

            return users
                .Where(u => u.CreateDate.Value.Year == year)  
                .ToList();
        }

        public async Task<int> SoftDeleteUserAsync(int userId)
        {
            var user = await GetUserByIdAsync(userId);
            if(user != null)
            {
                user.IsDeleted = 1;
               var result =  await context.SaveChangesAsync();
                return result;
            }
            return 0;
        }

        public async Task<bool> UpdateOtpUser(string email, string otpCode)
        {
            var checkUser = await context.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
            if (checkUser != null)
            {
                checkUser.Otp = otpCode;
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<int> UpdateUserAsync(User user)
        {
             context.Users.Update(user);
            return  await context.SaveChangesAsync();

        }
    }
}
