using System.ComponentModel.DataAnnotations;

namespace SarjuPos.API.DTOs
{
    public class RegisterOutletDto
    {
        [Required]
        public string OutletName { get; set; } = string.Empty;
        public string? Address { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public string? FullName { get; set; }
    }

    public class LoginDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int? OutletId { get; set; }
        public string? Token { get; set; }
    }
}
