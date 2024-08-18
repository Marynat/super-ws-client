using System.Net.WebSockets;

namespace super_ws.client.Humble;

public interface IClientWebSocket
{
    Task ConnectAsync(Uri uri, CancellationToken cancellationToken);
    Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken);
    Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken);
    Task CloseAsync(WebSocketCloseStatus closeStatus, string? statusDescription, CancellationToken cancellationToken);

    string? CloseStatusDescription { get; }
    WebSocketCloseStatus? CloseStatus { get; }
}