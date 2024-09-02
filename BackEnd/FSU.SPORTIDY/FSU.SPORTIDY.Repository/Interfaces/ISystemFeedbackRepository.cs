using FSU.SPORTIDY.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Repository.Interfaces
{
    public interface ISystemFeedbackRepository
    {
        public Task<List<SystemFeedback>> GetSystemFeedbackByUserId(int userId);
        public Task<(int TotalFeedbacks, int TotalImages, int TotalVideos, int TotalRatings)> GetFeedbackDashboard();
    }
}
