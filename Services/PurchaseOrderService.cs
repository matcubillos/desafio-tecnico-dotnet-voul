using Microsoft.EntityFrameworkCore;
using PruebaTecnicaDotNetApi7.Dtos.PurchaseOrder;
using PruebaTecnicaDotNetApi7.Models;
using PruebaTecnicaDotNetApi7.Persistence;
using PruebaTecnicaDotNetApi7.Repository;

namespace PruebaTecnicaDotNetApi7.Services
{
    public class PurchaseOrderService
    {
        // Inyeccion de dependencias para el repositorio y servicios relacionados
        private readonly PurchaseOrderRepository _repository;
        private readonly PurchaseOrderItemService _purchaseOrderItemService;
        private readonly ProductService _productService;
        public PurchaseOrderService(PurchaseOrderRepository repository,
                                    PurchaseOrderItemService purchaseOrderItemService,
                                    ProductService productService)
        {
            _repository = repository;
            _purchaseOrderItemService = purchaseOrderItemService;
            _productService = productService;
        }

        public async Task<IEnumerable<PurchaseOrderDto>> GetAllPurchaseOrdersAsync()
        {
            var purchaseOrders = await _repository.GetAllPurchaseOrdersAsync();
            var purchaseOrderDtos = purchaseOrders.Select(po => new PurchaseOrderDto
            {
                Id = po.Id,
                ClientName = po.ClientName,
                CreatedAt = po.CreatedAt,
                TotalPriceUSD = po.TotalPriceUSD
            });
            return purchaseOrderDtos;
        }

        public async Task<PurchaseOrderDto> CreatePurchaseOrderAsync(CreatePurchaseOrderDto purchaseOrderDto)
        {
            //creacion de la orden de compra, para luego obtener el Id y poder crear los items de la orden de compra
            var purchaseOrder = new PurchaseOrder
            {
                ClientName = purchaseOrderDto.ClientName,
            };

            var createdPurchaseOrder = await _repository.CreatePurchaseOrderAsync(purchaseOrder);

            decimal totalPrice = 0;
            // creacion de los items de la orden de compra, utilizando el Id de la orden de compra creada(createdPurchaseOrder.Id)
            foreach (var item in purchaseOrderDto.Items)
            {
                // verificar si el producto existe y obtener su precio
                var product = await _productService.GetProductByIdAsync(item.ProductId);
                if (product == null)
                {
                    throw new Exception($"Product with Id: {item.ProductId}, not found.");
                }
                // calcular el total del item y acumularlo al total de la orden de compra
                var itemTotal = item.Quantity * product.PriceUSD;
                totalPrice += itemTotal;
                Console.WriteLine($"totalPrice: {totalPrice} for item with ProductId: {item.ProductId}, Quantity: {item.Quantity}, Unit Price: {product.PriceUSD}");

                // Crear el item de la orden de compra
                var purchaseOrderItem = new PurchaseOrderItem
                {
                    PurchaseOrderId = createdPurchaseOrder.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product.PriceUSD
                };
                await _purchaseOrderItemService.CreatePurchaseOrderItemAsync(purchaseOrderItem);
            }
            //BONUS
            Console.WriteLine($"Precio total antes de descuentos: {totalPrice}");
            if (totalPrice > 500)
            {
                Console.WriteLine($"Descuento por monto >500 Iniciado. Antes: {totalPrice:C}");
                decimal discountPercentage = 10m;
                decimal discountedTotal = ApplyDiscount(totalPrice, discountPercentage); // aplicar el descuento a precio total mayor a 500, 10% (variable decimal discount)
                totalPrice = discountedTotal; // aplicar el descuento al total de la orden de compra
                Console.WriteLine($" → Precio total con descuento: {discountedTotal:C}");

            }
            if (purchaseOrderDto.Items.Count > 5)
            {
                Console.WriteLine($"Descuento por mas de 5 productos diferentes: Antes: {totalPrice:C}");
                decimal discountPercentage = 5m;
                decimal discountedTotal = ApplyDiscount(totalPrice, discountPercentage);//aplicar el descuento a precio total con mas de 5 items, 5% (variable decimal discount)
                totalPrice = discountedTotal; // aplicar el descuento al total de la orden de compra
                Console.WriteLine($" → Precio total con descuento: {discountedTotal:C}");
            }

            // actualizar el total de la orden de compra con el total calculado
            createdPurchaseOrder.TotalPriceUSD = Math.Round(totalPrice, 2);

            // guardar la orden de compra actualizada con el total
            await _repository.UpdatePurchaseOrderAsync(createdPurchaseOrder);

            var purchaseOrderDtoResult = new PurchaseOrderDto
            {
                Id = createdPurchaseOrder.Id,
                ClientName = createdPurchaseOrder.ClientName,
                CreatedAt = createdPurchaseOrder.CreatedAt,
                TotalPriceUSD = createdPurchaseOrder.TotalPriceUSD
            };
            return purchaseOrderDtoResult;
        }

        public async Task<PurchaseOrderDto?> GetPurchaseOrderByIdAsync(int id)
        {
            var purchaseOrder = await _repository.GetPurchaseOrderByIdAsync(id);
            if (purchaseOrder == null)
            {
                return null;
            }

            var purchaseOrderDto = new PurchaseOrderDto
            {
                Id = purchaseOrder.Id,
                ClientName = purchaseOrder.ClientName,
                CreatedAt = purchaseOrder.CreatedAt,
                TotalPriceUSD = purchaseOrder.TotalPriceUSD
            };
            return purchaseOrderDto;
        }

        public async Task<bool> UpdatePurchaseOrderAsync(int id, UpdatePurchaseDto updatePurchaseOrderDto)
        {
            var purchaseOrder = await _repository.GetPurchaseOrderByIdAsync(id);
            if (purchaseOrder == null)
            {
                return false;
            }
            purchaseOrder.ClientName = updatePurchaseOrderDto.ClientName;
            var updated = await _repository.UpdatePurchaseOrderAsync(purchaseOrder);
            return updated;
        }

        public async Task<bool> DeletePurchaseOrderAsync(int id)
        {
            var deleted = await _repository.DeletePurchaseOrderAsync(id);
            return deleted;
        }

        private static decimal ApplyDiscount(decimal TotalPriceUSD, decimal percentage)
        {
            decimal discountAmount = TotalPriceUSD * (percentage / 100m);
            decimal discountedTotal = TotalPriceUSD - discountAmount;
            return discountedTotal;
        }
    }
}
