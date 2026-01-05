using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Common.Behaviors;

namespace TaskManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(configuration => {
            configuration.RegisterServicesFromAssembly(assembly);

            // Enable for all Requests
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        // Find and Register all Validator files
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}