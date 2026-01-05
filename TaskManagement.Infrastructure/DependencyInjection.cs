using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Infrastructure.Data;
using TaskManagement.Application.Common;

namespace TaskManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IMongoDbContext, MongoDbContext>();
        return services;
    }
}