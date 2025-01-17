using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskRepository: IRepository<TaskManagement.Domain.Entities.Task>
    {
        void Update(TaskManagement.Domain.Entities.Task entity);
    }
}
