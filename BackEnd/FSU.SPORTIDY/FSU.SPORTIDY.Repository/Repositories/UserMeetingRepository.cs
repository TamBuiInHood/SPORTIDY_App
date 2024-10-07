using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Repository.Repositories
{
    public class UserMeetingRepository : GenericRepository<UserMeeting>, IUserMeetingRepository
    {
        public UserMeetingRepository(SportidyContext context) : base(context)
        {
        }
    }
}
