using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Repository.Repositories
{
    public class FriendShipRepository : GenericRepository<Friendship>, IFriendShipRepository
    {
        public FriendShipRepository(SportidyContext context) : base(context)
        {
        }
    }
}
