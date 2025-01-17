using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Services.Interface
{
    public interface ITaskService
    {
        IEnumerable<TaskManagement.Domain.Entities.Task> GetAllTasks();
        TaskManagement.Domain.Entities.Task GetTaskById(int id);
        void CreateTask(TaskManagement.Domain.Entities.Task villa);
        void UpdateTask(TaskManagement.Domain.Entities.Task villa);
        bool DeleteTask(int id);
    }
}
