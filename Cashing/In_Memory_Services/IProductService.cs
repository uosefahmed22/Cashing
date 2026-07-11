using Cashing.Dto;

namespace Cashing.In_Memory_Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetProductsAsync();
        Task<IEnumerable<ProductResponse>> GetProductsUsingCacheAsync_OldWay();
        Task<IEnumerable<ProductResponse>> GetProductsUsingCacheAsync_NewWay();
    }
}
