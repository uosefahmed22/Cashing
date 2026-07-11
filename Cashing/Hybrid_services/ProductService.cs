using AutoMapper;
using Cashing.Data;
using Cashing.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;

namespace Cashing.Hybrid_services
{
    public class ProductService(CashingDb cashingDb, HybridCache hybridCache, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<ProductResponse>> GetProductsAsync()
        {
            var products = await hybridCache.GetOrCreateAsync("products",
                async ct =>
                {
                    var productsFromDb = await cashingDb.Products.ToListAsync(ct);
                    var productResponses = mapper.Map<IEnumerable<ProductResponse>>(productsFromDb);
                    Console.WriteLine("Cache Hit");
                    return productResponses;
                });
            return products;
        }
    }
}
