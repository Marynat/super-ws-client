namespace super_ws.client.Messages;

public class QuotesMessage : SuperMessageBase
{

    public List<QuotePrice> D { get; set; } = new List<QuotePrice>();
}
