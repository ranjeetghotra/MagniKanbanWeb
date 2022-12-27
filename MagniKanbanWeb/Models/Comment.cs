namespace MagniKanbanWeb.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public int? UserId { get; set; }
        public string? Text { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
