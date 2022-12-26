namespace MagniKanbanWeb.Models.Requests
{
    public class CommentRequest
    {
        public int CardId { get; set; }
        public string? Text { get; set; }
    }
}
