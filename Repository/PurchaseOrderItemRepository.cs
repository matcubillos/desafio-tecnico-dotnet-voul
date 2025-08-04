using PruebaTecnicaDotNetApi7.Models;
using PruebaTecnicaDotNetApi7.Persistence;

namespace PruebaTecnicaDotNetApi7.Repository
{
    public class PurchaseOrderItemRepository
    {
        private readonly AppDbContext _context;
        public PurchaseOrderItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PurchaseOrderItem> CreatePurchaseOrderItemAsync(PurchaseOrderItem purchaseOrderItem)
        {
            _context.PurchaseOrderItems.Add(purchaseOrderItem);
            await _context.SaveChangesAsync();
            return purchaseOrderItem;
        }
    }
}
