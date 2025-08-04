using PruebaTecnicaDotNetApi7.Dtos.PurchaseOrderItem;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaDotNetApi7.Dtos.PurchaseOrder
{
    public class CreatePurchaseOrderDto
    {
        [Required(ErrorMessage = "Client name is required") ]
        public string ClientName { get; set; } = string.Empty;
        public List<CreatePurchaseOrderItemDto> Items { get; set; } = new List<CreatePurchaseOrderItemDto>();

    }
}
