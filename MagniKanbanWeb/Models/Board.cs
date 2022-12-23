using System.ComponentModel;

namespace MagniKanbanWeb.Models
{
    public class Board
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public int? ProjectId { get; set; }
        public ICollection<CardsModel>? Cards { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
