using System.ComponentModel.DataAnnotations;

namespace SarjuPos.API.Models
{
    public class Category : BaseTenantEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        // One category can have many products
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }

    public class Product : BaseTenantEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string? CategoryName { get; set; } // For simple lookup
        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        public int Stock { get; set; }

        public string? ImagePath { get; set; }

        public decimal TaxPercent { get; set; }

        public bool IsTaxIncluded { get; set; } = true;
    }
}
