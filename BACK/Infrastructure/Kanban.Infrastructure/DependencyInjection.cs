using Kanban.Application.Common.Interfaces;
using Kanban.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kanban.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<IKanbanDbContext, KanbanDbContext>(options => options.UseSqlite());

        return services;
    }
}
