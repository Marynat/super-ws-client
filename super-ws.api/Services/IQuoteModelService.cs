using super_ws.api.Model;

namespace super_ws.api.Services
{
    public interface IQuoteModelService
    {
        Task<QuoteModel> GetModelForQuoteAsync(string quoteName);
        Task<IEnumerable<QuoteMinuteModel>> GetMinuteModelForQuoteInRangeAsync(string quoteName, long from, long to);
    }
}