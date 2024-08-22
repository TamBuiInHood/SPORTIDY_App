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
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(SportidyContext context) : base(context)
        {
        }

        public async Task AddRoleAsync(Role newRole)
        {
            await context.Roles.AddAsync(newRole);
            await context.SaveChangesAsync();
        }

        public async Task<Role?> GetRoleById(int id)
        {
            return await context.Roles.FirstOrDefaultAsync(x => x.RoleId == id);    
        }
        public async Task<Role?> GetRoleByName(string roleName)
        {
            return await context.Roles.FirstOrDefaultAsync(x => x.RoleName.ToLower().Equals(roleName.ToLower()));
        }

    }
}
