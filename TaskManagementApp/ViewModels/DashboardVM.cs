namespace TaskManagement.Web.ViewModels
{
    public class DashboardVM
    {
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int PendingTasks { get; set; }
        public int OverdueTasks { get; set; }
        public List<TaskManagement.Domain.Entities.Task>? RecentTasks { get; set; }
    }
}
