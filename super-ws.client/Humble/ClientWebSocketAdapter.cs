using System.Net.WebSockets;

namespace super_ws.client.Humble;

public class ClientWebSocketAdapter(ClientWebSocket webSocket) : IClientWebSocket
{
    public string? CloseStatusDescription { get => webSocket.CloseStatusDescription; }
    public WebSocketCloseStatus? CloseStatus { get => webSocket.CloseStatus; }

    public async Task CloseAsync(WebSocketCloseStatus closeStatus, string? statusDescription, CancellationToken cancellationToken)
    {
        await webSocket.CloseAsync(closeStatus, statusDescription, cancellationToken);
    }

    public async Task ConnectAsync(Uri uri, CancellationToken cancellationToken)
    {
        await webSocket.ConnectAsync(uri, cancellationToken);
    }

    public async Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
    {
        return await webSocket.ReceiveAsync(buffer, cancellationToken);
    }

    public Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
    {
        return webSocket.SendAsync(buffer, messageType, endOfMessage, cancellationToken);
    }
}
