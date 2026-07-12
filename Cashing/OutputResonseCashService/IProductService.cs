using Cashing.Dto;
using Cashing.Responses;

namespace Cashing.OutputResonseCashService
{
    public interface IProductService
    {
        Task<PagedResult<ProductResponse>> GetProductsAsync(int pageNumber, int pageSize);
        Task<ProductResponse> GetProductByIdAsync(int id);
    }
}
