using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TaskManagement.Application.Services.Interface;
using TaskManagement.Web.ViewModels;
using TaskManagementApp.Models;

namespace TaskManagementApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITaskService _taskService;
        public HomeController(ILogger<HomeController> logger, ITaskService taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }

        public IActionResult Index()
        {
            var tasks = _taskService.GetAllTasks();
            foreach (var task in tasks)
            {
                task.StatusText = task.StatusList.FirstOrDefault(s => s.Value == task.Status)?.Text ?? "Unknown Status";
            }

            var model = new DashboardVM
            {
                TotalTasks = tasks.Count(),
                CompletedTasks = tasks.Count(t => t.StatusText == "Completed"),
                PendingTasks = tasks.Count(t => t.StatusText == "Pending"),
                OverdueTasks = tasks.Count(t => t.DueDate < DateOnly.FromDateTime(DateTime.Now) && t.StatusText != "Completed"),
                RecentTasks = tasks.OrderByDescending(t => t.CreatedDate).Take(5).ToList()
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
