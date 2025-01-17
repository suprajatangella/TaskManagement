using Microsoft.AspNetCore.SignalR;

namespace TaskManagement.Web.Hubs
{
    public class TaskHub : Hub
    {
        public async Task UpdateTaskStatus(string taskId, string status)
        {
            // Broadcast the updated status to all connected clients
            await Clients.All.SendAsync("ReceiveTaskUpdate", taskId, status);
        }
    }
}
