namespace super_ws.client.Messages
{
    public class QuotePrice
    {
        /// <summary>
        /// Nazwa
        /// </summary>
        public string S { get; set; } = string.Empty;
        /// <summary>
        /// Cena ask (cena kupna)
        /// </summary>
        public decimal A { get; set; } = decimal.Zero;
        /// <summary>
        /// Cena bid (cena kupna)
        /// </summary>
        public decimal B { get; set; } = decimal.Zero;
        /// <summary>
        /// Timestamp
        /// </summary>
        public long T { get; set; } = long.MinValue;
    }
}