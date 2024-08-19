using FSU.SPORTIDY.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Repository.Interfaces
{
    public interface IRoleRepository
    {
        public Task<Role?> GetRoleById(int id); 
        public Task<Role?> GetRoleByName(string roleName);
        public Task AddRoleAsync(Role newRole);

    }
}
