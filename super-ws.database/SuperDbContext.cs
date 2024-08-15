using Microsoft.EntityFrameworkCore;

namespace super_ws.database
{
    public class SuperDbContext(DbContextOptions options) : DbContext(options)
    {

    }
}
