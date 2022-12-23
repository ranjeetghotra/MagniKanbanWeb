namespace MagniKanbanWeb.Models
{
    public class CardsModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public int? BoardId { get; set; }
        public string? TaskId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
