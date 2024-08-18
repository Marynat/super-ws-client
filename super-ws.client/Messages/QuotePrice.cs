using System.Text.Json.Serialization;

namespace super_ws.client.Messages
{
    public class QuotePrice
    {
        /// <summary>
        /// Nazwa
        /// </summary>
        [JsonPropertyName("s")]
        public string S { get; set; } = string.Empty;
        /// <summary>
        /// Cena ask (cena kupna)
        /// </summary>
        [JsonPropertyName("a")]
        public decimal A { get; set; } = decimal.Zero;
        /// <summary>
        /// Cena bid (cena kupna)
        /// </summary>
        [JsonPropertyName("b")]
        public decimal B { get; set; } = decimal.Zero;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public long T { get; set; } = long.MinValue;
    }
}