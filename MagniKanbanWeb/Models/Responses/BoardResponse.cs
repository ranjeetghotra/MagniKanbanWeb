namespace MagniKanbanWeb.Models.Responses
{
    public class BoardResponse
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public ICollection<CardsModel>? Cards { get; set; }
    }
}
