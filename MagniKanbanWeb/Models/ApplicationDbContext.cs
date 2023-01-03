using Microsoft.EntityFrameworkCore;
using MagniKanbanWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MagniKanbanWeb.Models;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder
            .Entity<Card>()
            .Property(e => e.Tags)
            .HasConversion(
                 new ValueConverter<string[], string>(
                    x => string.Join(";", x),
                    x => x.Split(';', StringSplitOptions.RemoveEmptyEntries)
                    )
                 );
        modelBuilder
            .Entity<Card>()
            .Property(e => e.Assignees)
            .HasConversion(
                 new ValueConverter<string[], string>(
                    x => string.Join(";", x),
                    x => x.Split(';', StringSplitOptions.RemoveEmptyEntries)
                    )
                 );
        modelBuilder.Entity<Project>()
             .HasMany(j => j.Boards)
             .WithOne()
             .OnDelete(DeleteBehavior.Cascade);
    }

    public DbSet<Card> Cards => Set<Card>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Board> Boards => Set<Board>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Checklist> Checklists => Set<Checklist>();
    public DbSet<ChecklistItem> ChecklistItems => Set<ChecklistItem>();
    public DbSet<FileDetails> File => Set<FileDetails>();
    public DbSet<Timeline> Timelines => Set<Timeline>();
    public DbSet<Tag> Tags { get; set; }
}
