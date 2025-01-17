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
    public class TaskRepository : Repository<TaskManagement.Domain.Entities.Task>, ITaskRepository
    {
        private readonly ApplicationDbContext _db;

        public TaskRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(TaskManagement.Domain.Entities.Task entity)
        {
            _db.Tasks.Update(entity);
        }
    }
}
