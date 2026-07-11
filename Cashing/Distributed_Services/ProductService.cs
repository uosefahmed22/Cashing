using Cashing.Data;
using Cashing.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Cashing.Distributed_Services
{
    public class ProductService(CashingDb cashingDb, IDistributedCache cache) : IProductService
    {
        public async Task<IEnumerable<ProductResponse>> GetProductsAsync()
        {
            var cacheKey = "products";
            var cachedProducts = cache.GetString(cacheKey);

            if (!string.IsNullOrEmpty(cachedProducts))
            {
                Console.WriteLine("Cache hit");
                var productResponses = JsonSerializer.Deserialize<IEnumerable<ProductResponse>>(cachedProducts);
                return productResponses;
            }

            var products = await cashingDb.Products.ToListAsync();
            var productResponsesToCache = products.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            });

            var serializedProducts = JsonSerializer.Serialize(productResponsesToCache);

            await cache.SetStringAsync(cacheKey, serializedProducts,
                new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });

            return productResponsesToCache;
        }
    }
}
