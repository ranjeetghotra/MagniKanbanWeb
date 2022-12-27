using Microsoft.Build.Framework;

namespace MagniKanbanWeb.Models.Requests
{
    public class FileRequest
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
