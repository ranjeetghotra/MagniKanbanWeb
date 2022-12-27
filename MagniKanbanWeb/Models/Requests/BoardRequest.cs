using System.ComponentModel.DataAnnotations;

namespace MagniKanbanWeb.Models.Requests
{
    public class BoardRequest
    {
        [Required]
        public int? ProjectId { get; set; }
        [Required]
        public string? Title { get; set; }
    }
}
