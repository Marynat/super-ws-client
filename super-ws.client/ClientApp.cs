using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using super_ws.client.Messages;
using super_ws.client.Quotes;
using super_ws.database;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace super_ws.client;

public class ClientApp(SuperDbContext dbContext, IEnumerable<IQuote> wsConnections, ClientWebSocket webSocket, IConfiguration configuration, ILogger<ClientApp> logger) : IClientApp
{

    public async Task RunAsync()
    {

        await CreateConnectionAsync();
        SubscribeMessage sm = new SubscribeMessage("/subscribe/addlist");
        foreach (IQuote quote in wsConnections)
        {
            sm.D.Add(quote.Name);
        }
        await SendSubsribtionMessageAsync(sm);
        await ReceiveMessagesAsync();

    }

    private async Task CreateConnectionAsync()
    {
        var url = configuration.GetSection("ServiceUrl").Value;
        if (url != null)
        {
            logger.LogInformation($"Creating a connetion to : {url}");
            await webSocket.ConnectAsync(new Uri(url), CancellationToken.None);
            logger.LogInformation("Connected!");
        }
        else
        {
            logger.LogInformation("Please provide the service connection string in configuration file");
        }
    }

    private async Task SendSubsribtionMessageAsync(SubscribeMessage sm)
    {
        var message = JsonSerializer.Serialize(sm);
        var bytes = Encoding.UTF8.GetBytes(message);
        logger.LogInformation($"Sending subsribtion message: \n{message}");
        await webSocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
    }

    private async Task ReceiveMessagesAsync()
    {
        logger.LogInformation("Starting to receive messages");
        var buffer = new byte[1024 * 4];
        while (true)
        {
            try
            {
                var result = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer),
                    CancellationToken.None);

                if (result.MessageType ==
                WebSocketMessageType.Close)
                {
                    break;
                }

                var message = Encoding.UTF8.GetString(
                    buffer, 0, result.Count);
                JsonSerializer.Deserialize<QuotesMessage>(message);
                logger.LogInformation(message);
            }
            catch (WebSocketException ex)
            {
                logger.LogError(webSocket.CloseStatus.ToString());
                logger.LogError(webSocket.CloseStatusDescription);
                break;
            }
        }
    }
}

public interface IClientApp
{
    public Task RunAsync();
}