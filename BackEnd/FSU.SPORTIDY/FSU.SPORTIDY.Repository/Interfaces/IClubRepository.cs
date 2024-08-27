using FSU.SPORTIDY.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Repository.Interfaces
{
    public interface IClubRepository
    {
        public Task<List<UserClub>> GetClubJoinedByUserId(int userId); 
        public Task<List<UserMeeting>> GetMeetingsByClubId(int clubId);

        public Task<bool> JoinedClub(int userId, int clubId);   

        public Task<bool> InsertToUserClub(UserClub userClub);

        public Task<List<Club>> GetAllClub(Expression<Func<Club, bool>> filter = null!,
            Func<IQueryable<Club>, IOrderedQueryable<Club>> orderBy = null!,
            int? pageIndex = null, 
            int? pageSize = null);
    }
}
