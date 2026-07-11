using Cashing.Dto;

namespace Cashing.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetProductsAsync();
        Task<IEnumerable<ProductResponse>> GetProductsUsingCacheAsync_OldWay();
        Task<IEnumerable<ProductResponse>> GetProductsUsingCacheAsync_NewWay();
    }
}
