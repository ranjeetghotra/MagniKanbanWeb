namespace MagniKanbanWeb.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public int? BoardId { get; set; }
        public int? Order { get; set; }
        public string[] Assignees { get; set; } = { };
        public ICollection<Timeline>? Timeline { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Checklist>? Checklists { get; set; }
        public ICollection<FileDetails>? Files { get; set; }
        public string[] Tags { get; set; } = { };
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
