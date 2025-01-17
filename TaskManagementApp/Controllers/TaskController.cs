using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TaskManagement.Application.Services.Interface;
using TaskManagement.Domain.Entities;
using TaskManagement.Web.Hubs;

namespace TaskManagement.Web.Controllers
{
    public class TaskController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITaskService _taskService;
        private readonly ITaskCommentService _taskCommentService;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public TaskController(ITaskService taskService,ITaskCommentService taskCommentService, IHubContext<NotificationHub> hubContext, UserManager<ApplicationUser> userManager)
        {
            _taskService = taskService;
            _hubContext = hubContext;
            _userManager = userManager;
            _taskCommentService = taskCommentService;
        }

        public IActionResult Index(string sortOrder)
        {
            var tasks= _taskService.GetAllTasks();
           

            foreach (var task in tasks)
            {
                task.AssignedList = GetUsers();
                task.AssignedToText = task.AssignedList.FirstOrDefault(a=>a.Value== task.AssignedTo)?.Text ?? "Not Assigned"; 
                task.PriorityText = task.PriorityList.FirstOrDefault(p => p.Value == task.Priority)?.Text ?? "Unknown Priority";
                task.StatusText = task.StatusList.FirstOrDefault(s => s.Value == task.Status)?.Text ?? "Unknown Status";
            }

            // Sorting logic
            tasks = sortOrder switch
            {
                "priority_desc" => tasks.OrderByDescending(t => t.Priority).ToList(),
                "priority" => tasks.OrderBy(t => t.Priority).ToList(),
                _ => tasks.OrderBy(t => t.PriorityText).ToList() // Default sorting by Name
            };

            ViewBag.CurrentSort = sortOrder;

            return View(tasks);
        }

        public IActionResult Operations()
        {
            var tasks = _taskService.GetAllTasks();

            foreach (var task in tasks)
            {
                task.AssignedList = GetUsers();
                task.PriorityText = task.PriorityList.FirstOrDefault(p => p.Value == task.Priority)?.Text ?? "Unknown Priority";
            }

            return View(tasks);
        }

        public IActionResult Create()
        {
            TaskManagement.Domain.Entities.Task task = new TaskManagement.Domain.Entities.Task();
            task.AssignedList = GetUsers();

            return View(task);
        }
        [HttpPost]
        public IActionResult Create(TaskManagement.Domain.Entities.Task task)
        {
            if (ModelState.IsValid)
            {
                _taskService.CreateTask(task);
                TempData["success"] = "The Task has been created Successfully.";
                return RedirectToAction(nameof(Index));
            }

            return View(task);
        }

        public IActionResult Edit(int id)
        {
            var task = _taskService.GetTaskById(id);

            if (task == null)
            {
                return RedirectToAction("Error", "Home");
            }

            task.AssignedList = GetUsers();

            return View(task);
        }
        [HttpPost]
        public IActionResult Edit(TaskManagement.Domain.Entities.Task task)
        {
            if (ModelState.IsValid)
            {
                _taskService.UpdateTask(task);
                TempData["success"] = "The Task has been updated Successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        public IActionResult Delete(int id)
        {
            var task = _taskService.GetTaskById(id);
            if (task == null)
            {
                return RedirectToAction("Error", "Home");
            }

            task.AssignedList = GetUsers();

            task.AssignedToText = task.AssignedList.FirstOrDefault(a => a.Value == task.AssignedTo)?.Text ?? "Not Assigned";
            task.PriorityText = task.PriorityList.FirstOrDefault(p => p.Value == task.Priority)?.Text ?? "Unknown Priority";
            task.StatusText = task.StatusList.FirstOrDefault(s => s.Value == task.Status)?.Text ?? "Unknown Status";
            return View(task);
        }
        [HttpPost]
        public IActionResult Delete(TaskManagement.Domain.Entities.Task task)
        {
            if (task != null)
            {
                _taskService.DeleteTask(task.Id);
                TempData["success"] = "The Task has been deleted Successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Failed to delete the task.";
            }
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> AssignTask([FromBody] JsonElement data)
        {
            string taskId = data.GetProperty("taskId").ToString(); 
            var task = _taskService.GetTaskById(Convert.ToInt16(taskId));

            if (task == null)
            {
                return RedirectToAction("Error", "Home");
            }

            task.AssignedTo = data.GetProperty("userId").ToString();
            _taskService.UpdateTask(task);

            // Notify user about task assignment
            await _hubContext.Clients.User(task.AssignedTo).SendAsync("ReceiveNotification", $"New task assigned: {task.Title}");
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeTaskStatus([FromBody] JsonElement data)
        {
            // Update the database with the new status
            string taskId = data.GetProperty("taskId").ToString();
            var task = _taskService.GetTaskById(Convert.ToInt16(taskId));

            if (task == null)
            {
                return NotFound();
            }

            task.Status = data.GetProperty("status").ToString(); 
            _taskService.UpdateTask(task);

            // Notify clients via SignalR
            await _hubContext.Clients.All.SendAsync("ReceiveTaskUpdate", task.Id, task.Status);

            return Ok(new { success = true, message = "Task status updated successfully." });
            
        }
        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] JsonElement data)
        {
            // Update the database with the new status
            string taskId = data.GetProperty("taskId").ToString();
            var task = _taskService.GetTaskById(Convert.ToInt32(taskId));

            if (task == null)
            {
                return NotFound();
            }
            TaskComment comment = new TaskComment();
            comment.TaskId = task.Id;
            comment.UserId = task.AssignedTo;
            comment.CommentDate = DateTime.Now;
            task.AssignedList = GetUsers();
            comment.CommentText = task.AssignedList.FirstOrDefault(a => a.Value == task.AssignedTo)?.Text ?? "Not Assigned" + "is completed the issue";
            _taskCommentService.CreateTaskComment(comment);
            // Notify user about a new comment
            await _hubContext.Clients.User(task.AssignedTo).SendAsync("ReceiveNotification", $"New comment on Task {comment.TaskId}: {comment.CommentText}");
            return Ok(new { success = true, message = "Task comments created successfully." });
        }

        public IEnumerable<SelectListItem> GetUsers()
        {
            return _userManager.Users.Select(user => new SelectListItem
            {
                Value = user.Id, // Set the Value property to the user's ID
                Text = user.Name // Set the Text property to the user's Name
            });
        }
    }
}
