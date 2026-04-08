using System.ComponentModel.DataAnnotations;

namespace SarjuPos.API.Models
{
    public class Outlet : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public bool IsActive { get; set; } = true;

        // One outlet can have many users
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }

    public class User : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "Waiter"; // Owner, Manager, Waiter

        public string? FullName { get; set; }

        // Many users belong to one outlet
        // For Owner role, OutletId can be null (meaning they see all)
        public int? OutletId { get; set; }
        public virtual Outlet? Outlet { get; set; }

        public string? Permissions { get; set; } // JSON or comma separated string
    }
}
