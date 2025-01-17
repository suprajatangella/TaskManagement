using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ITaskRepository Task { get; }
        ITaskCommentRepository TaskComment { get; }
        INotificationRepository Notification { get; }
        IApplicationUserRepository User { get; }
        void Save();
    }
}
