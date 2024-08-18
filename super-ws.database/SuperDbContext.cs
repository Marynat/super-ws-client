using Microsoft.EntityFrameworkCore;
using super_ws.database.Entity;

namespace super_ws.database
{
    public class SuperDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<QuoteEntity> Quotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuoteEntity>()
                .HasKey(e => e.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
