using Microsoft.Build.Framework;

namespace MagniKanbanWeb.Models.Requests
{
    public class ChecklistItemRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int ChecklistId { get; set; }
    }
}
