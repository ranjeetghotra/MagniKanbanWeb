using Microsoft.EntityFrameworkCore;
using MagniKanbanWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MagniKanbanWeb.Models;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<CardsModel> Cards => Set<CardsModel>();
    public DbSet<CommentsModel> Comments => Set<CommentsModel>();
    public DbSet<Board> Boards => Set<Board>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<FileDetails> File { get; set; }
}
