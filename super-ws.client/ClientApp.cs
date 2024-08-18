using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using super_ws.client.Humble;
using super_ws.client.Messages;
using super_ws.client.Quotes;
using super_ws.database.Entity;
using super_ws.database.Repository;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace super_ws.client;

public class ClientApp(ISuperDbContextRepository dbContext, IEnumerable<IQuote> wsConnections, IClientWebSocket webSocket, IConfiguration configuration, ILogger<ClientApp> logger, IMapper mapper) : IClientApp
{

    private SubscribeMessage sm = new SubscribeMessage("/subscribe/addlist");
    public async Task RunAsync()
    {

        await CreateConnectionAsync();
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

    private async Task ReconnectAsync(CancellationToken token)
    {
        logger.LogError(webSocket.CloseStatus.ToString());
        logger.LogError(webSocket.CloseStatusDescription);

        if (token.IsCancellationRequested)
        {
            logger.LogError("Could not reconect to the server");
            return;
        }
        await CreateConnectionAsync();
        await SendSubsribtionMessageAsync(sm);
    }

    private async Task ReceiveMessagesAsync()
    {
        CancellationTokenSource cancellationTokenSource = new(TimeSpan.FromMinutes(1));
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
                var qoutesMessage = JsonSerializer.Deserialize<QuotesMessage>(message);
                await SaveQuote(qoutesMessage);
                logger.LogInformation(message);
                cancellationTokenSource = new(TimeSpan.FromMinutes(1));
            }
            catch (WebSocketException)
            {
                await Task.Delay(5000, cancellationTokenSource.Token);
                await ReconnectAsync(cancellationTokenSource.Token);
            }
        }
    }

    private async Task SaveQuote(QuotesMessage message)
    {
        var entities = mapper.Map<HashSet<QuoteEntity>>(message.D);
        foreach (var entity in entities)
        {
            if (await dbContext.AnyQuotesAsync(entity.Id)) continue;
            await dbContext.AddEntityAsync(entity);
        }
        await dbContext.SaveChangesAsync();
    }
}

public interface IClientApp
{
    public Task RunAsync();
}