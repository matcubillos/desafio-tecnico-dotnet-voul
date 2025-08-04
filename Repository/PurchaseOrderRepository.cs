using Microsoft.EntityFrameworkCore;
using PruebaTecnicaDotNetApi7.Models;
using PruebaTecnicaDotNetApi7.Persistence;

namespace PruebaTecnicaDotNetApi7.Repository
{
    public class PurchaseOrderRepository
    {
        private readonly AppDbContext _context;
        public PurchaseOrderRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<PurchaseOrder>> GetAllPurchaseOrdersAsync()
        {
            return await _context.PurchaseOrders.ToListAsync();
        }

        public async Task<PurchaseOrder> CreatePurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            _context.PurchaseOrders.Add(purchaseOrder);
            await _context.SaveChangesAsync();
            return purchaseOrder;
        }

        public async Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(int id)
        {
            return await _context.PurchaseOrders.FindAsync(id);
        }

        public async Task<bool> UpdatePurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            _context.PurchaseOrders.Update(purchaseOrder);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePurchaseOrderAsync(int id)
        {
            var purchaseOrder = await _context.PurchaseOrders.FindAsync(id);
            if (purchaseOrder == null)
            {
                return false;
            }
            _context.PurchaseOrders.Remove(purchaseOrder);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
