using Microsoft.VisualBasic.FileIO;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagniKanbanWeb.Models
{
    public class FileDetails
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[]? FileData { get; set; }
        public int? CardId { get; set; }
    }
}
