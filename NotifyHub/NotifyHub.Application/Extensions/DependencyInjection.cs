using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotifyHub.Application.Pipeline;
using System.Reflection;

namespace NotifyHub.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(typeof(MediatR.ServiceCollectionExtensions).Assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
