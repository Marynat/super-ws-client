using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace super_ws.database.Entity;

public class QuoteMinuteEntity
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
                    Time = DateTimeOffset.Parse(keys[1]);
                }
            }
        }
    }
    public DateTimeOffset Time { get; set; } = new DateTimeOffset();
    public string Name { get; set; } = string.Empty;
    [Column(TypeName = "money")]
    public decimal High { get; set; } = decimal.Zero;
    [Column(TypeName = "money")]
    public decimal Low { get; set; } = decimal.Zero;
    [Column(TypeName = "money")]
    public decimal Open { get; set; } = decimal.Zero;
    [Column(TypeName = "money")]
    public decimal Close { get; set; } = decimal.Zero;
    public int Volume { get; set; } = 0;
}
