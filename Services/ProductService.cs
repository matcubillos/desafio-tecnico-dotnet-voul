using PruebaTecnicaDotNetApi7.Dtos.Product;
using PruebaTecnicaDotNetApi7.Models;
using PruebaTecnicaDotNetApi7.Repository;

namespace PruebaTecnicaDotNetApi7.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;
        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            var productDtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                PriceUSD = p.PriceUSD
            });
            return productDtos;
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return null;
            }
            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                PriceUSD = product.PriceUSD
            };
            return productDto;
        }

        public async Task<Product> CreateProductAsync(CreateProductDto createProductDto)
        {
            var product = new Product
            {
                Name = createProductDto.Name,
                PriceUSD = createProductDto.PriceUSD,
            };
            var createdProduct = await _productRepository.CreateProductAsync(product);
            return createdProduct;
        }
    }
}
