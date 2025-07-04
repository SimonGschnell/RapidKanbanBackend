using Microsoft.EntityFrameworkCore;
using RapidKanban.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace RapidKanban.Infrastructure.Context;

public class AppDbContext: DbContext
{
    
    DbSet<KanbanTask> Tasks { get; set; }
    DbSet<Userstory> Userstories { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KanbanTask>().Property(t => t.Status).HasConversion<string>();
        modelBuilder.Entity<Userstory>().HasMany<KanbanTask>(u=>u.Tasks).WithOne(t => t.Userstory)
            .HasForeignKey(u => u.UserstoryId);
        
    }
}