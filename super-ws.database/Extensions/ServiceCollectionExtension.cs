using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace super_ws.database.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddSuperDbContext(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<SuperDbContext>(options => options.UseSqlServer(config.GetConnectionString("SuperWs")));
        return services;
    }
}
