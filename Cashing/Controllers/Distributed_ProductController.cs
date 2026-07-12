using Cashing.Distributed_Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cashing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Distributed_ProductController(IProductService _productService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProductsWithCaching()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }
    }
}
