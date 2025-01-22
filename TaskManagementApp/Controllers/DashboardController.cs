using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Services.Interface;
using TaskManagementApp.Controllers;

namespace TaskManagement.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ITaskService _taskService;
        public DashboardController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        public IActionResult Index()
        {
            var tasks = _taskService.GetAllTasks();

            foreach (var task in tasks)
            {
                task.StatusText = task.StatusList.FirstOrDefault(s => s.Value == task.Status)?.Text ?? "Unknown Status";
            }
            var taskCounts = new
            {
                Completed = tasks.Count(t => t.StatusText == "Completed"),
                Pending = tasks.Count(t => t.StatusText == "Pending"),
                Overdue = tasks.Count(t => t.DueDate < DateOnly.FromDateTime(DateTime.Now) && t.StatusText != "Completed")
            };

            return View(taskCounts);
        }
    }
}
