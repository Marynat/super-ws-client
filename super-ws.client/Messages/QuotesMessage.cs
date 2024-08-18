using System.Text.Json.Serialization;

namespace super_ws.client.Messages;

public class QuotesMessage : SuperMessageBase
{
    [JsonPropertyName("d")]
    public List<QuotePrice> D { get; set; } = new List<QuotePrice>();
}
