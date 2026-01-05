using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<MongoDbContext>();

        return services;
    }
}