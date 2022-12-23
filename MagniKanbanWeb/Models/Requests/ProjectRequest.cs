using Microsoft.Build.Framework;

namespace MagniKanbanWeb.Models.Requests
{
    public class ProjectRequest
    {
        [Required]
        public string Title { get; set; }
    }
}
