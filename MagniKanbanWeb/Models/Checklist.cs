namespace MagniKanbanWeb.Models
{
    public class Checklist
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? CardId { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
