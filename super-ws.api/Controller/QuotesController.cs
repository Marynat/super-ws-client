using Microsoft.AspNetCore.Mvc;
using super_ws.api.Model;
using super_ws.api.Services;

namespace super_ws.api.Controller;

[Route("quotes")]
[ApiController]
public class QuotesController(IQuoteModelService _service) : ControllerBase
{

    [HttpGet]
    public async Task<IEnumerable<QuoteMinuteModel>> GetQuotes(string symbol, long from, long to)
    {
        return await _service.GetModelForQuoteInRangeAsync(symbol, from, to);
    }

}
