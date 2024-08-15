using System.Text.Json.Serialization;

namespace super_ws.client.Messages;

public class SubscribeMessage : SuperMessageBase
{
    public SubscribeMessage(string link)
    {
        P = link;
    }

    /// <summary>
    /// List of quotes to subsribe to;
    /// </summary>
    [JsonPropertyName("d")]
    public List<string> D { get; set; } = new List<string>();
}
