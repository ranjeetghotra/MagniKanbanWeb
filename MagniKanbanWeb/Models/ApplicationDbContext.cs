using Microsoft.EntityFrameworkCore;
using MagniKanbanWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MagniKanbanWeb.Models;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Card> Cards => Set<Card>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Board> Boards => Set<Board>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<FileDetails> File { get; set; }
    public DbSet<Tag> Tags { get; set; }
}
