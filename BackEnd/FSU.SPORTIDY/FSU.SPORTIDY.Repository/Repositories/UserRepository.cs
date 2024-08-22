using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
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
        //private readonly IConfiguration _configuration;
        public UserRepository(SportidyContext context) : base(context)
        {
            _context = context;
            //_configuration = config;
        }
    }
}
