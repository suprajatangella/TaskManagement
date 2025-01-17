using Microsoft.AspNetCore.SignalR;

namespace TaskManagement.Web.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task NotifyUser(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", message);
        }
    }
}
