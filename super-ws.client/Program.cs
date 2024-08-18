using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using super_ws.client;
using super_ws.client.Extensions;

var _host = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
{
    services.AddClient(hostContext.Configuration);

}).Build();

var app = _host.Services.GetRequiredService<IClientApp>();
await app.RunAsync();
