namespace super_ws.client.Quotes;

public class EthQuote : IQuote
{
    public EthQuote()
    {
        Name = "ETHUSD";
    }

    public string Name { get; }
}
