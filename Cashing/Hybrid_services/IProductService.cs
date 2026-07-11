using Cashing.Dto;

namespace Cashing.Hybrid_services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetProductsAsync();
    }
}
