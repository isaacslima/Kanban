using Kanban.Application.Common.Interfaces;
using Kanban.Domain;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Infrastructure.Data;

public class KanbanDbContext : DbContext, IKanbanDbContext
{
    public KanbanDbContext(DbContextOptions<KanbanDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Card> Cards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
