using Cashing.Dto;

namespace Cashing.Distributed_Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetProductsAsync();
    }
}
