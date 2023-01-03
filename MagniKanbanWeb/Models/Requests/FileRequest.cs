using Microsoft.Build.Framework;

namespace MagniKanbanWeb.Models.Requests
{
    public class FileRequest
    {
        public int? CardId { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}
