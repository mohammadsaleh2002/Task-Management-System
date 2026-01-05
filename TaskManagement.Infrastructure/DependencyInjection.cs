using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Infrastructure.Data;
using TaskManagement.Application.Common;
using TaskManagement.Application.Common.Interfaces; 
using TaskManagement.Infrastructure.Authentication; 

namespace TaskManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IMongoDbContext, MongoDbContext>();

        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        return services;
    }
}