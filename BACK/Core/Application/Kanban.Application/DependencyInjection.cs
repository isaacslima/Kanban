using Kanban.Application.Common.Interfaces.Services;
using Kanban.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Kanban.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICardService, CardService>();
        return services;
    }
}
