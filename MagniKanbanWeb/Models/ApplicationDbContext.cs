using Microsoft.EntityFrameworkCore;
using MagniKanbanWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MagniKanbanWeb.Models;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<TasksModel> Tasks => Set<TasksModel>();
    public DbSet<CommentsModel> Comments => Set<CommentsModel>();
    public DbSet<BoardModel> Boards => Set<BoardModel>();
}
