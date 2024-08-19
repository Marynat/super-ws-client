using super_ws.api.Pages;
using super_ws.database.Repository;

namespace super_ws.api.Services
{
    public class QuoteModelService(ISuperDbContextRepository superDb) : IQuoteModelService
    {
        public async Task<QuoteModel> GetModelForQuoteAsync(string quoteName)
        {
            var currentDate = DateTimeOffset.Now;
            currentDate.AddSeconds(-currentDate.Second);
            QuoteModel quote = new();
            for (int i = 0; i < 30; i++)
            {
                var entites = await superDb.GetQuotesForMinute(quoteName, currentDate.AddMinutes(-i - 1).ToUnixTimeSeconds(), currentDate.AddMinutes(-i).ToUnixTimeSeconds());
                if (entites != null && entites.Count() > 0)
                {
                    quote.open.Add(entites.FirstOrDefault().BidPrice);
                    quote.close.Add(entites.LastOrDefault().BidPrice);
                    quote.x.Add(currentDate.AddMinutes(-i));
                    quote.high.Add(entites.Select(e => e.BidPrice).Max());
                    quote.low.Add(entites.Select(e => e.BidPrice).Min());
                    quote.volume = entites.Count();
                }
            }
            return quote;
        }
    }
}
