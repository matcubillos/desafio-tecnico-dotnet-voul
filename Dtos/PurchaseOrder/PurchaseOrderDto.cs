namespace PruebaTecnicaDotNetApi7.Dtos.PurchaseOrder
{
    public class PurchaseOrderDto
    {
        public int Id { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public decimal TotalPriceUSD { get; set; }
    }
}
