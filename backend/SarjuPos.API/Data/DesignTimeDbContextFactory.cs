using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SarjuPos.API.Services;

namespace SarjuPos.API.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=SarjuPosDb;Username=postgres;Password=heera11**");

            // Mock Tenant Service for design time
            return new ApplicationDbContext(optionsBuilder.Options, new MockTenantService());
        }
    }

    public class MockTenantService : ITenantService
    {
        public int? GetOutletId() => null;
        public string GetRole() => "Owner";
    }
}
