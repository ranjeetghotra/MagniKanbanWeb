using MagniKanbanWeb.Models;
using MagniKanbanWeb.Models.Responses;

namespace MagniKanbanWeb.Services
{
    public class TimelineService
    {
        private readonly ApplicationDbContext dbContext;
        public TimelineService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
