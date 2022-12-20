using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace MagniKanbanWeb.Models
{
    public class TasksModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
