﻿namespace MagniKanbanWeb.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
