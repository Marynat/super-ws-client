using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using super_ws.client;
using super_ws.client.Extensions;
using super_ws.client.Humble;
using super_ws.client.Quotes;
using super_ws.database.Extensions;
using System.Net.WebSockets;

var _host = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
{
    services.AddSuperDbContext(hostContext.Configuration);
    services.AddClient(hostContext.Configuration);
    services.AddTransient<ClientWebSocket>();
    services.AddTransient<IClientWebSocket, ClientWebSocketAdapter>();
    services.AddSingleton<IQuote, BtcQuote>();
    services.AddSingleton<IQuote, EthQuote>();

    services.AddSingleton<IClientApp, ClientApp>();

}).Build();

var app = _host.Services.GetRequiredService<IClientApp>();
await app.RunAsync();
