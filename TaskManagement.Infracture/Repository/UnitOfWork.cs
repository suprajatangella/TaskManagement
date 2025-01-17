using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Infrastructure.Repository;
using TaskManagement.InfraStructure.Data;

namespace TaskManagement.InfraStructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ITaskRepository Task { get; private set; }

        public ITaskCommentRepository TaskComment { get; private set; }

        public INotificationRepository Notification { get; private set; }

        public IApplicationUserRepository User { get; private set; }

        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Task = new TaskRepository(_db);
            TaskComment = new TaskCommentRepository(_db);
            Notification = new NotificationRepository(_db);
            User= new ApplicationUserRepository(_db);
        }

        void IUnitOfWork.Save()
        {
            _db.SaveChanges();
        }
    }
}
