using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaDotNetApi7.Models
{
    public class PurchaseOrderItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PurchaseOrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // navigation properties for relationships(PurchaseOrderId and ProductId)
        public virtual PurchaseOrder PurchaseOrder { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;

    }
}
