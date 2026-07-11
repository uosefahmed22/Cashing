using Cashing.Hybrid_services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cashing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Hybrid_ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public Hybrid_ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet()]
        public IActionResult Get()
        {   
            var products = _productService.GetProductsAsync().Result;
            return Ok(products);
        }
    }
}
