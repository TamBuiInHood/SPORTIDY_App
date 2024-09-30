using FSU.SPORTIDY.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Repository.Interfaces
{
    public interface IUserRepository
    {
        public Task<User?> GetUserByEmailAsync(string email);
        public Task<User?> GetUserByIdAsync(int userId);
        public Task AddUserAsync(User newUser);

        public Task<bool> UpdateOtpUser(string email, string otpCode);
        public Task<int> UpdateUserAsync(User user);
        public Task<int> SoftDeleteUserAsync(int userId);
        public Task<List<User>> GetAllUsersByRole(string roleName);
        public Task<List<User>> GetUsersByYear(int year);
    }
}

