using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaDotNetApi7.Dtos.Product
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public decimal PriceUSD { get; set; }
    }
}
