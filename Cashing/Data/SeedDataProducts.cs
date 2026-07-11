using Cashing.Models;
using Microsoft.EntityFrameworkCore;

namespace Cashing.Data
{
    public class SeedDataProducts
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CashingDb(
                serviceProvider.GetRequiredService<DbContextOptions<CashingDb>>()))
            {
                context.Database.Migrate();

                if (context.Products.Any())
                {
                    return; 
                }

                context.Products.AddRange(
                    new Product
                    {
                        Name = "Product 1",
                        Price = 9.99M,
                        Description = "This is the first product.",
                        CreatedAt = DateTime.UtcNow,
                        LastUpdatedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Name = "Product 2",
                        Price = 19.99M,
                        Description = "This is the second product.",
                        CreatedAt = DateTime.UtcNow,
                        LastUpdatedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Name = "Product 3",
                        Price = 29.99M,
                        Description = "This is the third product.",
                        CreatedAt = DateTime.UtcNow,
                        LastUpdatedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Name = "Product 4",
                        Price = 39.99M,
                        Description = "This is the fourth product.",
                        CreatedAt = DateTime.UtcNow,
                        LastUpdatedAt = DateTime.UtcNow
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
