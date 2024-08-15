namespace super_ws.client.Quotes;

public class BtcQuote : IQuote
{
    public BtcQuote()
    {
        Name = "BTCUSD";
    }
    public string Name { get; }
}
