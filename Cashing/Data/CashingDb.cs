using Cashing.Models;
using Microsoft.EntityFrameworkCore;

namespace Cashing.Data
{
    public class CashingDb : DbContext
    {
        public CashingDb(DbContextOptions<CashingDb> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Price).HasColumnType("decimal(18, 2)");
            });
        }
    }
}
