using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using TaskManagement.Application.Services.Interface;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Web.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly UserManager<ApplicationUser> _userManager;
        public NotificationsController(INotificationService notificationService, UserManager<ApplicationUser> userManager)
        {
            _notificationService = notificationService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {

            // Get the logged-in user
            var user = await _userManager.GetUserAsync(User);

            // Get the user's ID
            var userId = user?.Id;

          
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if user is not authenticated
            }

            var notifications = _notificationService.GetAllNotifications(userId);

            return View(notifications); // Pass notifications to the view
        }

        [HttpPost]
        public IActionResult MarkAsRead(int id)
        {
            _notificationService.MarkAsRead(id);
            return RedirectToAction("GetNotifications");
        }

        [HttpGet]
        public IActionResult CreateNotification()
        {
            Notification notification = new Notification();
            notification.UserList = GetUsers();
            return View(notification);
        }

        [HttpPost]
        public IActionResult CreateNotification(Notification notification)
        {
            if (ModelState.IsValid)
            {
                _notificationService.CreateNotification(notification);
                return RedirectToAction("GetNotifications");
            }
            return View(notification);
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
