using Microsoft.AspNetCore.Mvc.RazorPages;
using super_ws.database.Repository;

namespace super_ws.api.Pages
{
    public class BTCUSDModel(ISuperDbContextRepository superDb) : PageModel
    {

        public QuoteModel quote = new(); //This should be a DTO

        public async Task OnGetAsync()
        {
            var currentDate = DateTimeOffset.Now;
            for (int i = 0; i < 30; i++)
            {
                var ble = currentDate.AddMinutes(-i - 1).ToUnixTimeSeconds();
                var entites = await superDb.GetQuotesForMinute("BTCUSD", currentDate.AddMinutes(-i - 1).ToUnixTimeSeconds(), currentDate.AddMinutes(-i).ToUnixTimeSeconds());
                if (entites != null && entites.Count() > 0)
                {
                    quote.open.Add(entites.FirstOrDefault().BidPrice);
                    quote.close.Add(entites.LastOrDefault().BidPrice);
                    quote.x.Add(currentDate.AddMinutes(-i));
                    quote.high.Add(entites.Select(e => e.BidPrice).Max());
                    quote.low.Add(entites.Select(e => e.BidPrice).Min());
                }
            }
        }
    }

    public class QuoteModel()
    {
        public List<DateTimeOffset> x { get; set; } = new List<DateTimeOffset>();
        public List<decimal> high { get; set; } = new List<decimal>();
        public List<decimal> low { get; set; } = new List<decimal>();
        public List<decimal> open { get; set; } = new List<decimal>();
        public List<decimal> close { get; set; } = new List<decimal>();

        public string type { get; set; } = "ohlc";
        public string xaxis { get; set; } = "x";
        public string yaxis { get; set; } = "y";

        public Deacreasing deacreasing { get; set; } = new();
        public Increasing increasing { get; set; } = new();
    }

    public class Deacreasing()
    {
        public Line line { get; set; } = new Line() { color = "#7F7F7F" };
    }
    public class Increasing()
    {
        public Line line { get; set; } = new Line() { color = "#17BECF" };
    }
    public class Line()
    {
        public string color { get; set; }
    }
}
