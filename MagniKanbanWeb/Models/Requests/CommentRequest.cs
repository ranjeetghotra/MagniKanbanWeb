using System.ComponentModel.DataAnnotations;

namespace MagniKanbanWeb.Models.Requests
{
    public class CommentRequest
    {
        [Required]
        public int CardId { get; set; }
        [Required]
        public string? Text { get; set; }
    }
}
