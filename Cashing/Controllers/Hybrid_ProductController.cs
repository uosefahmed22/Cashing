using Cashing.Hybrid_services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cashing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Hybrid_ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get()
        {   
            var products = productService.GetProductsAsync().Result;
            return Ok(products);
        }
    }
}
