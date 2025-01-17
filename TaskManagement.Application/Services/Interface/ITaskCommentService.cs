using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Services.Interface
{
    public interface ITaskCommentService
    {
        IEnumerable<TaskComment> GetAllTaskComments();
        TaskComment GetTaskCommentById(int id);
        void CreateTaskComment(TaskComment taskComment);
        void UpdateTaskComment(TaskComment taskComment);
        bool DeleteTaskComment(int id);
    }
}
