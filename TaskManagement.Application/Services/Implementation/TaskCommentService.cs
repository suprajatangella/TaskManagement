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
    public class TaskCommentService : ITaskCommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskCommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateTaskComment(TaskComment taskComment)
        {
            _unitOfWork.TaskComment.Add(taskComment);
            _unitOfWork.Save();
        }

        public bool DeleteTaskComment(int id)
        {
            var taskComment = _unitOfWork.TaskComment.Get(t => t.Id == id);

            if (taskComment != null)
            {
                _unitOfWork.TaskComment.Remove(taskComment);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public IEnumerable<TaskComment> GetAllTaskComments()
        {
            return _unitOfWork.TaskComment.GetAll();
        }

        public TaskComment GetTaskCommentById(int id)
        {
            return _unitOfWork.TaskComment.Get(u=>u.Id==id);
        }

        public void UpdateTaskComment(TaskComment taskComment)
        {
            _unitOfWork.TaskComment.Update(taskComment);
            _unitOfWork.Save();
        }
    }
}
