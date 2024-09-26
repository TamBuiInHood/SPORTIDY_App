using FSU.SPORTIDY.Common.Utils;
using FSU.SPORTIDY.Repository.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Repository.Interfaces
{
    public interface INotificationRepository
    {
        public Task<List<Notification>> GetAllNotificationsByUserIdAsync(int userId);
        public Task<List<Notification>> GetAllNotificationsByEmailAsync(string email);
        public Task<bool> MarkNotificationIsReadByNotificationId(int notificationId);
        public Task<bool> MarkNotificationIsReadByUserId(int userId);
    }
}
