using Kanban.Application.Common.Interfaces;
using Kanban.Application.Common.Interfaces.Persistence;
using Kanban.Infrastructure.Data;
using Kanban.Infrastructure.Data.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kanban.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddSingleton<DataContext>();

        services.AddScoped<ICardRepository, CardRepository>();

        return services;
    }
}
