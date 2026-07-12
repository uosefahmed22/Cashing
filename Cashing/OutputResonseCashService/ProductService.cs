using AutoMapper;
using Cashing.Data;
using Cashing.Dto;
using Cashing.Responses;
using Microsoft.EntityFrameworkCore;

namespace Cashing.OutputResonseCashService
{
    public class ProductService(CashingDb cashingDb, IMapper mapper) : IProductService
    {
        public async Task<ProductResponse> GetProductByIdAsync(int id)
        {
            var entity = await cashingDb.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (entity == null)
            {
                return null;
            }
            var productResponse = mapper.Map<ProductResponse>(entity);
            return productResponse;
        }

        public async Task<PagedResult<ProductResponse>> GetProductsAsync(int pageNumber = 1, int pageSize = 10)
        {
            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var entities = await cashingDb.Products
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
                
            var totalCount = await cashingDb.Products.CountAsync();

            var productResponses = mapper.Map<IEnumerable<ProductResponse>>(entities);

            return new PagedResult<ProductResponse>{
                Items = productResponses,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}