using Microsoft.EntityFrameworkCore;
using PruebaTecnicaDotNetApi7.Models;
using PruebaTecnicaDotNetApi7.Persistence;

namespace PruebaTecnicaDotNetApi7.Repository
{
    public class ProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<Product> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
