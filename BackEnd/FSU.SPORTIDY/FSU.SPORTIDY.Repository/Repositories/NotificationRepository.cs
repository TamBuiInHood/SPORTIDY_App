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
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(SportidyContext context) : base(context)
        {
        }

        public async Task<List<Notification>> GetAllNotificationsByEmailAsync(string email)
        {
            var result = await context.Notifications
                    .Include(x => x.User)
                    .Where(x => x.User.Email.ToLower().Equals(email))
                    .ToListAsync();
            return result;
        }

        public async Task<List<Notification>> GetAllNotificationsByUserIdAsync(int userId)
        {
            var result = await context.Notifications.Include(x => x.User).Where(x => x.UserId == userId).ToListAsync(); 
            return result;
        }

        public async Task<bool> MarkNotificationIsReadByNotificationId(int notificationId)
        {
            var notification = await context.Notifications.FirstOrDefaultAsync(x => x.NotificationId == notificationId);
            notification.IsRead = true;
            var result = await context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> MarkNotificationIsReadByUserId(int userId)
        {
            var listNotification = await context.Notifications
                .Include(x => x.User)
                .Where(x => x.User.UserId == userId).ToListAsync();

            foreach(var notification in listNotification)
            {
                 notification.IsRead = true;
            }
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
    }
}
