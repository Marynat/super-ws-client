using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using super_ws.database.Extensions;

namespace super_ws.client.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddClient(this IServiceCollection services, IConfiguration config)
    {
        services.AddSuperDbContext(config);
        return services;
    }

    public static IServiceCollection AddPublisher(this IServiceCollection services)
    {

        //services.AddTransient<QuotesPublisher>();

        return services;
    }
}
