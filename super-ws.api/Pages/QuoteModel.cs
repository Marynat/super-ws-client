namespace super_ws.api.Pages
{
    public class QuoteModel()
    {
        public List<DateTimeOffset> x { get; set; } = new List<DateTimeOffset>();
        public List<decimal> high { get; set; } = new List<decimal>();
        public List<decimal> low { get; set; } = new List<decimal>();
        public List<decimal> open { get; set; } = new List<decimal>();
        public List<decimal> close { get; set; } = new List<decimal>();

        public int volume { get; set; } = 0;

        public string type { get; set; } = "ohlc";
        public string xaxis { get; set; } = "x";
        public string yaxis { get; set; } = "y";

        public Deacreasing deacreasing { get; set; } = new();
        public Increasing increasing { get; set; } = new();
    }
}
