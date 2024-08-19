using Newtonsoft.Json;

namespace super_ws.api.Model
{
    public class QuoteMinuteModel
    {
        [JsonProperty("instrument")]
        public string Name { get; set; } = string.Empty;
        [JsonProperty("czas")]
        public DateTimeOffset Time { get; set; } = new DateTimeOffset();
        [JsonProperty("max")]
        public decimal High { get; set; } = decimal.Zero;
        [JsonProperty("min")]
        public decimal Low { get; set; } = decimal.Zero;
        [JsonProperty("cenaOtwarcia")]
        public decimal Open { get; set; } = decimal.Zero;
        [JsonProperty("cenaZamkniecia")]
        public decimal Close { get; set; } = decimal.Zero;
        [JsonProperty("wolumen")]
        public int Volume { get; set; } = 0;
    }
}
