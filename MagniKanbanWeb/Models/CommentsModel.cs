namespace MagniKanbanWeb.Models
{
    public class CommentsModel
    {
        public int Id { get; set; }
        public string? TaskId { get; set; }
        public string? UserId { get; set; }
        public string? Text { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
