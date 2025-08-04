using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaDotNetApi7.Dtos.Product;
using PruebaTecnicaDotNetApi7.Dtos.PurchaseOrder;
using PruebaTecnicaDotNetApi7.Models;
using PruebaTecnicaDotNetApi7.Services;

namespace PruebaTecnicaDotNetApi7.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/products")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct( CreateProductDto createProductDto )
        {
            try
            {
                var createdProduct = await _productService.CreateProductAsync(createProductDto);
                return (Ok(createdProduct));
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            try
                {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

    }
}
