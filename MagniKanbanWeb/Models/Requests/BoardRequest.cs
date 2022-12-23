using Microsoft.Build.Framework;

namespace MagniKanbanWeb.Models.Requests
{
    public class BoardRequest
    {
        [Required]
        public string? Title { get; set; }
    }
}
