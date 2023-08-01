using Kanban.Domain;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Application.Common.Interfaces;

public interface IKanbanDbContext
{
    DbSet<Card> Cards { get; set; }
}
