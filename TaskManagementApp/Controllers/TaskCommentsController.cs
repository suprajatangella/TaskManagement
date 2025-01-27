using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Services.Interface;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Web.Controllers
{
    public class TaskCommentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITaskCommentService _taskCommentService;
        private readonly UserManager<ApplicationUser> _userManager;
        public TaskCommentsController(IUnitOfWork unitOfWork, ITaskCommentService taskCommentService, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _taskCommentService = taskCommentService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            // Get the logged-in user's ID
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;  // id is the unique identifier
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account"); // Redirect if user is not logged in
            }

            // Fetch comments associated with tasks assigned to the logged-in user
            var userTaskComments = _unitOfWork.TaskComment
                .GetAll()
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.CommentDate)
                .ToList();

            foreach (var comment in userTaskComments)
            {
                comment.UserList = GetUsers();
            }

            return View(userTaskComments);
        }

        public IActionResult Edit(int id)
        {
            var comment = _taskCommentService.GetTaskCommentById(id);
            if (comment == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(comment);
        }
        [HttpPost]
        public IActionResult Edit(TaskComment comment)
        {
            if (ModelState.IsValid)
            {
                comment.UpdatedDate = DateOnly.FromDateTime(DateTime.Now);
                _taskCommentService.UpdateTaskComment(comment);
                TempData["success"] = "The Task Comment has been updated Successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        public IActionResult Delete(int id)
        {
            var comment = _taskCommentService.GetTaskCommentById(id);
            if (comment == null)
            {
                return RedirectToAction("Error", "Home");
            }

            //task.AssignedList = GetUsers();

            //task.AssignedToText = task.AssignedList.FirstOrDefault(a => a.Value == task.AssignedTo)?.Text ?? "Not Assigned";
            //task.PriorityText = task.PriorityList.FirstOrDefault(p => p.Value == task.Priority)?.Text ?? "Unknown Priority";
            //task.StatusText = task.StatusList.FirstOrDefault(s => s.Value == task.Status)?.Text ?? "Unknown Status";
            return View(comment);
        }
        [HttpPost]
        public IActionResult Delete(TaskComment comment)
        {
            if (comment != null)
            {
                _taskCommentService.DeleteTaskComment(comment.Id);
                TempData["success"] = "The Task Comment has been deleted Successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Failed to delete the task comment.";
            }
            return View(comment);
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
