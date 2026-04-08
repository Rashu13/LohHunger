using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SarjuPos.API.Models
{
    public class Purchase : BaseTenantEntity
    {
        [Required]
        [StringLength(200)]
        public string VendorName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
        public string? PaymentMethod { get; set; }
        public string? Status { get; set; } = "Completed";

        public virtual ICollection<PurchaseItem> Items { get; set; } = new List<PurchaseItem>();
    }

    public class PurchaseItem : BaseTenantEntity
    {
        [Required]
        public int PurchaseId { get; set; }
        [ForeignKey("PurchaseId")]
        public virtual Purchase? Purchase { get; set; }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class CreditNote : BaseTenantEntity
    {
        public int? OrderId { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public decimal Amount { get; set; }
        public string? Reason { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
    }

    public class PaymentTransaction : BaseTenantEntity
    {
        [Required]
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingBalance { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
    }
}
