namespace MagniKanbanWeb.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public string? UserId { get; set; }
        public string? Text { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
