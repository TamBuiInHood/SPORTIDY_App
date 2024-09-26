using FSU.SPORTIDY.Common.Role;
using FSU.SPORTIDY.Common.Utils;
using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Service.BusinessModel.NotificationModels;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.Interfaces
{
    public interface INotificationService
    {
        public Task<bool> AddNotificationByCustomerId(int customerId, NotificationModel notificationModel);

        public Task<bool> AddNotificationByRoleAsync(string roleName, NotificationListModel notificationModel);

        public Task<bool> AddNotificationByListCustomerId(List<int> userIds, NotificationListModel notificationModel);

        public Task<Notification> GetNotificationById(int id);

        public Task<List<Notification>> GetNotificationsByCustomerId(int customerId);

        public Task<List<Notification>> GetNotificationsByEmail(string email);

        public Task<bool> MarkAllUserNotificationIsReadByUserIdAsync(int userId);

        public Task<bool> MarkNotificationIsReadById(int notificationId);

    }
}
