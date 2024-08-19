using Microsoft.AspNetCore.Mvc.RazorPages;
using super_ws.api.Services;

namespace super_ws.api.Pages
{
    public class ETHUSDModel(IQuoteModelService service) : PageModel
    {
        public QuoteModel quote = new();

        public async Task OnGetAsync()
        {
            quote = await service.GetModelForQuoteAsync("ETHUSD");
        }
    }
}
