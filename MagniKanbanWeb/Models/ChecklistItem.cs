namespace MagniKanbanWeb.Models
{
    public class ChecklistItem
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? ChecklistId { get; set; }
        public bool Checked { get; set; } = false;
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
