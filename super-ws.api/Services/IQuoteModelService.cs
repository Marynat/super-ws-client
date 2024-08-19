using super_ws.api.Pages;

namespace super_ws.api.Services
{
    public interface IQuoteModelService
    {
        Task<QuoteModel> GetModelForQuoteAsync(string quoteName);
    }
}