using Cashing.Distributed_Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cashing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Distributed_ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public Distributed_ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsWithCaching()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }
    }
}
