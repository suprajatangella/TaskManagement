using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Services.Interface;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Services.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void CreateNotification(Notification notification)
        {
            _unitOfWork.Notification.Add(notification);
            _unitOfWork.Save();
        }

        public bool DeleteNotification(int id)
        {
            var notification = _unitOfWork.Notification.Get(n => n.Id == id);

            if (notification != null)
            {
                _unitOfWork.Notification.Remove(notification);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public IEnumerable<Notification> GetAllNotifications(string userId)
        {
            return  _unitOfWork.Notification.GetAll()
                .Where(n => n.UserId == userId && !n.IsRead)
            .OrderByDescending(n => n.CreatedAt);
        }

        public Notification GetNotificationById(int id)
        {
            return _unitOfWork.Notification.Get(n=> n.Id == id);
        }

        public void MarkAsRead(int id)
        {
            var notification =  _unitOfWork.Notification.Get(n=>n.Id==id);
            if (notification != null)
            {
                notification.IsRead = true;
                _unitOfWork.Notification.Update(notification);
                _unitOfWork.Save();
            }
        }

        public void UpdateNotification(Notification notification)
        {
            _unitOfWork.Notification.Update(notification);
            _unitOfWork.Save();
        }
    }
}
