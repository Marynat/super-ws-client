using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using super_ws.database.Entity;
using System.Net.Sockets;

namespace super_ws.database.Repository;

public class SuperDbContextRepository(SuperDbContext _context, ILogger<SuperDbContext> logger) : ISuperDbContextRepository
{

    public async Task AddEntityAsync<T>(T Entity)
    {
        if (Entity != null && !Equals(Entity, default(T)))
        {
            await _context.AddAsync(Entity);
        }
    }

    public async Task AddRangeAsync<T>(IEnumerable<T> quotes)
    {
        await _context.AddRangeAsync(quotes);
    }

    public async Task<IEnumerable<QuoteEntity>> GetAllQuotesAsync()
    {
        return await _context.Quotes.Select(q => q).ToListAsync();
    }

    public async Task<IEnumerable<QuoteEntity>> GetQuotesForMinute(string name, long minuteStart, long minuteEnd)
    {
        return await _context.Quotes.Where(q => q.Name == name && q.Time >= minuteStart && q.Time < minuteEnd).ToListAsync();
    }
    public async Task<IEnumerable<QuoteMinuteEntity>> GetQuoteMinutesForRange(string name, DateTimeOffset minuteStart, DateTimeOffset minuteEnd)
    {
        return await _context.QuoteMinutes.Where(q => q.Name == name && q.Time >= minuteStart && q.Time < minuteEnd).ToListAsync();
    }

    public async Task<QuoteEntity> GetEntity(string key)
    {
        return await _context.Quotes.FirstAsync(q => q.Id == key);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AnyQuotesAsync(string key)
    {
        return await _context.Quotes.AnyAsync(q => q.Id == key);
    }
    public async Task<bool> AnyQuoteMinutesAsync(string key)
    {
        return await _context.QuoteMinutes.AnyAsync(q => q.Id == key);
    }
}

public interface ISuperDbContextRepository
{
    Task AddEntityAsync<T>(T Entity);
    Task AddRangeAsync<T>(IEnumerable<T> quotes);
    Task SaveChangesAsync();
    Task<QuoteEntity> GetEntity(string key);
    Task<IEnumerable<QuoteEntity>> GetAllQuotesAsync();
    Task<IEnumerable<QuoteEntity>> GetQuotesForMinute(string name, long minuteStart, long minuteEnd);
    Task<IEnumerable<QuoteMinuteEntity>> GetQuoteMinutesForRange(string name, DateTimeOffset minuteStart, DateTimeOffset minuteEnd);
    Task<bool> AnyQuotesAsync(string key);
    Task<bool> AnyQuoteMinutesAsync(string key);
}