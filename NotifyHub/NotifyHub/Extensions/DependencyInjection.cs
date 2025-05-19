using NotifyHub.Infrastructure.Extensions;

namespace NotifyHub.Api.Extensions;

internal static class DependencyInjection
{
    public static IServiceCollection RegisterApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        return services;
    }
}
