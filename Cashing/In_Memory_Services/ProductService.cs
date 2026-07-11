using AutoMapper;
using Cashing.Data;
using Cashing.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Cashing.In_Memory_Services
{
    public class ProductService(CashingDb cashingDb, IMemoryCache cache, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<ProductResponse>> GetProductsAsync()
        {
            var products = await cashingDb.Products.ToListAsync();
            var productResponses = mapper.Map<IEnumerable<ProductResponse>>(products);
            return productResponses;

        }
        public async Task<IEnumerable<ProductResponse>> GetProductsUsingCacheAsync_OldWay()
        {
            var cacheKey = "products";
            if (cache.TryGetValue(cacheKey, out IEnumerable<ProductResponse> productResponses))
            {
                Console.WriteLine("Cache hit");
                return productResponses;
            }

            var products = await cashingDb.Products.ToListAsync();
            productResponses = mapper.Map<IEnumerable<ProductResponse>>(products);
            cache.Set(cacheKey, productResponses, TimeSpan.FromMinutes(5));
            return productResponses;
        }

        public Task<IEnumerable<ProductResponse>> GetProductsUsingCacheAsync_NewWay()
        {
            var cacheKey = "products";
            var productResponses = cache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                var products = cashingDb.Products.ToList();
                Console.WriteLine("Cache hit");
                return mapper.Map<IEnumerable<ProductResponse>>(products);
            });
            return Task.FromResult(productResponses);
        }

    }
}



