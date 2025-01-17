using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Repository;
using TaskManagement.InfraStructure.Data;

namespace TaskManagement.InfraStructure.Repository
{
    public class TaskCommentRepository : Repository<TaskComment>, ITaskCommentRepository
    {
        private readonly ApplicationDbContext _db;
        public TaskCommentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(TaskComment entity)
        {
            _db.TaskComments.Update(entity);
        }
    }
}
