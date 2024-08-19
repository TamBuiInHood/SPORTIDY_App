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
        public Task AddUserAsync(User newUser);
    }
}
