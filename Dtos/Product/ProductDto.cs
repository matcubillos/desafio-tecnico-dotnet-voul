namespace PruebaTecnicaDotNetApi7.Dtos.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal PriceUSD { get; set; }
    }
}
