using AutoMapper;
using FSU.SPORTIDY.Common.Role;
using FSU.SPORTIDY.Common.Utils;
using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.UnitOfWork;
using FSU.SPORTIDY.Service.BusinessModel.NotificationModels;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using FSU.SPORTIDY.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> AddNotificationByCustomerId(int customerId, NotificationModel notificationModel)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(customerId);
            if (user != null)
            {
                var notification = new Notification()
                {
                    NotificationCode = notificationModel.deviceToken,
                    IsRead = false,
                    Tiltle = notificationModel.title,
                    Message = notificationModel.message,
                    UserId = notificationModel.userId,
                    NotificationType = true,
                    InviteDate = DateTime.Now,
                };
                await _unitOfWork.NotificationRepository.Insert(notification);
                var result = await _unitOfWork.SaveAsync();
                return result > 0;
            }
            return false;
        }

        public async Task<bool> AddNotificationByListCustomerId(List<int> userIds, NotificationListModel notificationModel)
        {
            foreach(var userId in userIds)
            {
                var user =  await _unitOfWork.UserRepository.GetUserByIdAsync(userId);
                if (user != null)
                {
                        var newNoti = new Notification
                        {
                            NotificationCode = user.DeviceCode,
                            IsRead = false,
                            Tiltle = notificationModel.title,
                            Message = notificationModel.message,
                            UserId = user.UserId,
                            NotificationType = true,
                            InviteDate = DateTime.Now,
                        };
                    await _unitOfWork.NotificationRepository.Insert(newNoti);
                }
            }
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        public async Task<bool> AddNotificationByRoleAsync(string roleName, NotificationListModel notificationModel)
        {
            var users = await _unitOfWork.UserRepository.GetAllUsersByRole(roleName);
            if (users.Any())
            {
                
                foreach (var user in users)
                {
                    var newNoti = new Notification
                    {
                        NotificationCode = user.DeviceCode,
                        IsRead = false,
                        Tiltle = notificationModel.title,
                        Message = notificationModel.message,
                        UserId = user.UserId,
                        NotificationType = true,
                        InviteDate = DateTime.Now,
                    };
                    await _unitOfWork.NotificationRepository.Insert(newNoti);
                    
                }
                var result =  await _unitOfWork.SaveAsync();
                return result > 0;
            }
            return false;
        }

        public async Task<Notification> GetNotificationById(int id)
        {
           var notification = await _unitOfWork.NotificationRepository.GetByCondition(x => x.NotificationId == id, includeProperties: "User");
            return notification;
        }

        public async Task<List<Notification>> GetNotificationsByCustomerId(int customerId)
        {
            var listNotification = await _unitOfWork.NotificationRepository.GetAllNotificationsByUserIdAsync(customerId);
            return listNotification;
        }

        public async Task<List<Notification>> GetNotificationsByEmail(string email)
        {
            var listNotification = await _unitOfWork.NotificationRepository.GetAllNotificationsByEmailAsync(email);
            return listNotification;
        }

        public async Task<bool> MarkAllUserNotificationIsReadByUserIdAsync(int userId)
        {
            var result = await _unitOfWork.NotificationRepository.MarkNotificationIsReadByUserId(userId);
            return result;
        }

        public async Task<bool> MarkNotificationIsReadById(int notificationId)
        {
            var result = await _unitOfWork.NotificationRepository.MarkNotificationIsReadByNotificationId(notificationId);
            return result;
        }
    }
}
