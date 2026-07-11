using Cashing.Data;
using Cashing.In_Memory_Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cashing.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class In_Memory_ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public In_Memory_ProductController(IProductService productService)
        {
            _productService = productService;
        }
        //without caching
        [HttpGet("without-caching")]
        public async Task<IActionResult> GetProductsWithoutCaching()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }

        //with caching old way
        [HttpGet("with-caching-old-way")]
        public async Task<IActionResult> GetProductsWithCachingOldWay()
        {
            var products = await _productService.GetProductsUsingCacheAsync_OldWay();
            return Ok(products);
        }

        //with caching new way
        [HttpGet("with-caching-new-way")]
        public async Task<IActionResult> GetProductsWithCachingNewWay()
        {
            var products = await _productService.GetProductsUsingCacheAsync_NewWay();
            return Ok(products);
        }
    }
}
