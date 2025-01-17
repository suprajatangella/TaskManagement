using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Services.Interface;

namespace TaskManagement.Application.Services.Implementation
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateTask(Domain.Entities.Task task)
        {
            _unitOfWork.Task.Add(task);
            _unitOfWork.Save();
        }

        public bool DeleteTask(int id)
        {
            var task = _unitOfWork.Task.Get(t=>t.Id == id);

            if (task != null)
            {
                _unitOfWork.Task.Remove(task);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public IEnumerable<Domain.Entities.Task> GetAllTasks()
        {
            return _unitOfWork.Task.GetAll();
        }

        public Domain.Entities.Task GetTaskById(int id)
        {
            return _unitOfWork.Task.Get(u=>u.Id == id);
        }

        public void UpdateTask(Domain.Entities.Task task)
        {
            _unitOfWork.Task.Update(task);
            _unitOfWork.Save();
        }
    }
}
