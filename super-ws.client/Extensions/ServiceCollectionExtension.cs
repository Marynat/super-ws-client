using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using super_ws.client.Humble;
using super_ws.client.Quotes;
using super_ws.database.Extensions;
using System.Net.WebSockets;
using System.Reflection;

namespace super_ws.client.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddClient(this IServiceCollection services, IConfiguration config)
    {
        services.AddSuperDbContext(config);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddTransient<ClientWebSocket>();
        services.AddTransient<IClientWebSocket, ClientWebSocketAdapter>();
        services.AddSingleton<IQuote, BtcQuote>();
        services.AddSingleton<IQuote, EthQuote>();

        services.AddTransient<IClientApp, ClientApp>();
        return services;
    }

    public static IServiceCollection AddPublisher(this IServiceCollection services)
    {

        //services.AddTransient<QuotesPublisher>();

        return services;
    }
}
