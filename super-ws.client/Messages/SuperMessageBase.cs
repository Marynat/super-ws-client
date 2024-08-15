using System.Text.Json.Serialization;

namespace super_ws.client.Messages;

public class SuperMessageBase
{
    /// <summary>
    /// link
    /// </summary>
    [JsonPropertyName("p")]
    public string P { get; set; } = string.Empty;
}
