using PruebaTecnicaDotNetApi7.Models;
using PruebaTecnicaDotNetApi7.Repository;

namespace PruebaTecnicaDotNetApi7.Services
{
    public class PurchaseOrderItemService
    {
        private readonly PurchaseOrderItemRepository _repository;
        public PurchaseOrderItemService(PurchaseOrderItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<PurchaseOrderItem> CreatePurchaseOrderItemAsync(PurchaseOrderItem purchaseOrderItem)
        {
            var createdItem = await _repository.CreatePurchaseOrderItemAsync(purchaseOrderItem);
            return createdItem;
        }

    }
}
