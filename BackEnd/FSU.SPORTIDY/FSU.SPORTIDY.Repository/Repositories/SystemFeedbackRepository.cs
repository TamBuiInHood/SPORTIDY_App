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
    public class SystemFeedbackRepository : GenericRepository<SystemFeedback>, ISystemFeedbackRepository
    {
        public SystemFeedbackRepository(SportidyContext context) : base(context)
        {
        }

        public async Task<(int TotalFeedbacks, int TotalImages, int TotalVideos, int TotalRatings)> GetFeedbackDashboard()
        {
            // Đếm tổng số feedback, số feedback có images, và số feedback có rating
            int totalFeedbacks = await context.SystemFeedbacks.CountAsync();
            int totalImages = await context.SystemFeedbacks.CountAsync(f => !string.IsNullOrEmpty(f.ImageUrl));
            int totalVideos = await context.SystemFeedbacks.CountAsync(f => !string.IsNullOrEmpty(f.VideoUrl));
            int totalRatings = await context.SystemFeedbacks.CountAsync(f => f.Rating > 0);

            return (totalFeedbacks, totalImages, totalVideos, totalRatings);
        }

        public async Task<List<SystemFeedback>> GetSystemFeedbackByUserId(int userId)
        {
            var result = await context.SystemFeedbacks.Include(x => x.User).Where(x => x.UserId == userId).ToListAsync();
            return result;
        }
    }
}
