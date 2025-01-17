using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Services.Interface
{
    public interface INotificationService
    {
        IEnumerable<Notification> GetAllNotifications(string userId);
        Notification GetNotificationById(int id);
        void CreateNotification(Notification notification);
        void UpdateNotification(Notification notification);
        bool DeleteNotification(int id);
        void MarkAsReadAsync(int id);
    }
}
