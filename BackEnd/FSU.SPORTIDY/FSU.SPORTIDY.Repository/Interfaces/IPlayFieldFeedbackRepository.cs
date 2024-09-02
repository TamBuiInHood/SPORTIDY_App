using FSU.SPORTIDY.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Repository.Interfaces
{
    public interface IPlayFieldFeedbackRepository
    {
        public Task<List<PlayFieldFeedback>> GetPlayFieldFeedbackByOwnerId(int ownerId);  
        public Task<PlayField> GetPlayFieldByPlayFieldId(int playfieldId);  
        public Task<PlayFieldFeedback> GetNewestPlayFieldFeedback();  
        public Task<Booking> GetBookingByIdAsync(int bookingId);  
        public Task<PlayFieldFeedback> GetPlayFieldFeedbackByIdAsync(int id);  
    }
}
