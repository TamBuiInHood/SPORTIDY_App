using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Repository.Repositories
{
    public class ClubRepository : GenericRepository<Club>, IClubRepository
    {
        private readonly SportidyContext _sportidyContext;

        public ClubRepository(SportidyContext context) : base(context)
        {
            _sportidyContext = context;
        }

        public async Task<List<Club>> GetAllClub(Expression<Func<Club, bool>> filter = null!, Func<IQueryable<Club>, 
            IOrderedQueryable<Club>> orderBy = null!, 
            int? pageIndex = null, int? pageSize = null)
        {
            IQueryable<Club> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query.Include(x => x.UserClubs).ThenInclude(x => x.User).ThenInclude(x => x.Role);

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            // Implementing pagination
            if (pageIndex.HasValue && pageSize.HasValue)
            {
                // Ensure the pageIndex and pageSize are valid
                int validPageIndex = pageIndex.Value > 0 ? pageIndex.Value - 1 : 0;
                int validPageSize = pageSize.Value > 0 ? pageSize.Value : 10; // Assuming a default pageSize of 10 if an invalid value is passed

                query = query.Skip(validPageIndex * validPageSize).Take(validPageSize);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<List<UserClub>> GetClubJoinedByUserId(int userId)
        {
            var checkUser = await _sportidyContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);    
            if (checkUser == null)
            {
                return null;
            }
            var listClub = await _sportidyContext.UserClubs
                            .Include(x => x.Club)
                            .Include(x => x.User)
                            .Where(x => x.UserId == userId).ToListAsync();
            return listClub;
        }

        public async Task<List<UserMeeting>> GetMeetingsByClubId(int clubId)
        {
            var checkExistClub = await _sportidyContext.Clubs.FirstOrDefaultAsync(x => x.ClubId == clubId);
            if(checkExistClub == null)
            {
                return null;
            }

            var listMeetings = await _sportidyContext.UserMeetings
                                    .Include(x => x.Meeting)
                                    .Include(x => x.User)
                                    .Where(x => x.ClubId == clubId).ToListAsync();
            return listMeetings;
        }

        public async Task<bool> InsertToUserClub(UserClub userClub)
        {
            await _sportidyContext.UserClubs.AddAsync(userClub);
            var result = await _sportidyContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> JoinedClub(int userId, int clubId)
        {
            try
            {
                var checkUser = await _sportidyContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
                var checkClub = await _sportidyContext.Clubs.FirstOrDefaultAsync(x => x.ClubId == clubId);
                if (checkUser != null && checkClub != null)
                {
                    var newUserClub = new UserClub()
                    {
                        ClubId = clubId,
                        UserId = userId,
                        IsLeader = false
                    };
                    _sportidyContext.UserClubs.Add(newUserClub);
                    var result = await _sportidyContext.SaveChangesAsync();
                    return result > 0;
                }
                return false;
            }
            catch (Exception)
            {

                throw new Exception("User has joinned  this club");
            }
        }
    }
}
