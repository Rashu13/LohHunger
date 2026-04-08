using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SarjuPos.API.Models
{
    public class Order : BaseTenantEntity
    {
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; } = "Cash";

        [StringLength(50)]
        public string Status { get; set; } = "Completed";

        public int? TableId { get; set; }
        public virtual RestaurantTable? Table { get; set; }

        public bool IsOfficial { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }

    public class OrderItem : BaseTenantEntity
    {
        [Required]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual Product? Product { get; set; }

        [Required]
        [StringLength(200)]
        public string ProductName { get; set; } = string.Empty;

        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal TaxPercent { get; set; }
        public decimal TaxAmount { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Active";

        public string? CancelReason { get; set; }
    }
}
