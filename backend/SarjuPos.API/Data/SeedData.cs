using Microsoft.EntityFrameworkCore;
using SarjuPos.API.Models;
using BCrypt.Net;

namespace SarjuPos.API.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>(),
                serviceProvider.GetRequiredService<Services.ITenantService>()))
            {
                // Ensure database is created
                context.Database.EnsureCreated();

                // Look for any outlets
                if (context.Outlets.Any())
                {
                    return;   // DB has been seeded
                }

                // Initial Outlet
                var outlet = new Outlet
                {
                    Name = "Sarju POS - Head Office",
                    Address = "Main Street, City Center",
                };
                context.Outlets.Add(outlet);
                context.SaveChanges();

                // Admin/Owner User
                var admin = new User
                {
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("123"),
                    Role = "Owner",
                    FullName = "Super Admin",
                    OutletId = outlet.Id
                };
                context.Users.Add(admin);

                // Sample Categories
                var category = new Category { Name = "General", OutletId = outlet.Id };
                context.Categories.Add(category);
                context.SaveChanges();

                // Sample Product
                context.Products.Add(new Product
                {
                    Name = "Sample Product",
                    Price = 10.0m,
                    CategoryId = category.Id,
                    CategoryName = "General",
                    Stock = 100,
                    OutletId = outlet.Id
                });

                context.SaveChanges();
            }
        }
    }
}
