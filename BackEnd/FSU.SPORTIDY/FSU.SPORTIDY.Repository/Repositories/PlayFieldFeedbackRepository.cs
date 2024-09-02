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
    public class PlayFieldFeedbackRepository : GenericRepository<PlayFieldFeedback>, IPlayFieldFeedbackRepository
    {
        public PlayFieldFeedbackRepository(SportidyContext context) : base(context)
        {
        }

        public async Task<PlayField> GetPlayFieldByPlayFieldId(int playfieldId)
        {
            var playField = await context.PlayFields.FirstOrDefaultAsync(x => x.PlayFieldId == playfieldId);    
            return playField;   
        }

        public async Task<List<PlayFieldFeedback>> GetPlayFieldFeedbackByOwnerId(int ownerId)
        {
            var checkOwner = await context.Users.FirstOrDefaultAsync(x => x.UserId == ownerId); 
            if(checkOwner != null)
            {
                var listPlayFieldFeedback = await context.PlayFieldFeedbacks
                                                .Include(x => x.Booking)
                                                .ThenInclude(x => x.PlayField)
                                                .ThenInclude(x => x.User)
                                                .Where(x => x.Booking.PlayField.UserId == ownerId)
                                                .ToListAsync();
                return listPlayFieldFeedback;
            }
            return null;
        }

        public async Task<PlayFieldFeedback> GetNewestPlayFieldFeedback()
        {
            var result = await context.PlayFieldFeedbacks
                .Include(x => x.Booking)
                .ThenInclude(x => x.PlayField)
                .ThenInclude(x => x.User)
                .OrderByDescending(x => x.FeedbackId)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            var booking = await context.Bookings.FirstOrDefaultAsync(x => x.BookingId == bookingId);
            return booking;
        }

        public async Task<PlayFieldFeedback> GetPlayFieldFeedbackByIdAsync(int id)
        {
            var result = await context.PlayFieldFeedbacks
                .Include(x => x.Booking)
                .ThenInclude(x => x.PlayField)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.FeedbackId == id);
            return result;
        }
    }
}
