namespace MagniKanbanWeb.Models
{
    public class Project
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public ICollection<Board>? Boards { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
