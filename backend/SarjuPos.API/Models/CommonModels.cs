using System.ComponentModel.DataAnnotations;

namespace SarjuPos.API.Models
{
    public class Customer : BaseTenantEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Address { get; set; }
        public decimal TotalCredit { get; set; }
    }

    public class RestaurantTable : BaseTenantEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = "Available";
    }


    public class Expense : BaseTenantEntity
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string? Category { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
        public string? Note { get; set; }
    }

    public class AuditLog : BaseTenantEntity
    {
        [Required]
        public string Action { get; set; } = string.Empty;
        public string? Details { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
    }

    public class Staff : BaseTenantEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        [StringLength(100)]
        public string Role { get; set; } = "Staff";
        public string? Phone { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoiningDate { get; set; } = DateTime.UtcNow;
    }

    public class Subscription : BaseTenantEntity
    {
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? ProductName { get; set; }
        public decimal Amount { get; set; }
        public string? Frequency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? NextDate { get; set; }
        public string Status { get; set; } = "Active";
    }
}
