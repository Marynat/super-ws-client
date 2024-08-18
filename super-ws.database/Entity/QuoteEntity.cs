using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace super_ws.database.Entity;

public class QuoteEntity
{
    public string Id
    {
        get => $"{Name}|{Time}";
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                var keys = value.Split(' ');
                if (keys.Length > 1)
                {
                    Name = keys[0];
                    Time = long.Parse(keys[1]);
                }
            }
        }
    }
    /// <summary>
    /// Nazwa
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Cena ask (cena kupna)
    /// </summary>
    [Column(TypeName = "money")]
    public decimal AskPrice { get; set; } = decimal.Zero;
    /// <summary>
    /// Cena bid (cena kupna)
    /// </summary>
    [Column(TypeName = "money")]
    public decimal BidPrice { get; set; } = decimal.Zero;
    /// <summary>
    /// Timestamp
    /// </summary>
    public long Time { get; set; } = long.MaxValue;
}
