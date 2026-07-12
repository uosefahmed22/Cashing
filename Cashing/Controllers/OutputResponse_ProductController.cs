using Cashing.OutputResonseCashService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.Threading.Tasks;

namespace Cashing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutputResponse_ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet]
        [OutputCache(Duration = 10)]
        public async Task<IActionResult> Get()
        {
            var products = await productService.GetProductsAsync(1, 10);
            return Ok(products);
        }

        [HttpGet("{productId:int}")]
        [OutputCache(VaryByRouteValueNames = ["productId"], Duration = 10)]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = await productService.GetProductByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
