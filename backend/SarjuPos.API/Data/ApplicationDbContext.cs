using Microsoft.EntityFrameworkCore;
using SarjuPos.API.Models;
using SarjuPos.API.Services;

namespace SarjuPos.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ITenantService _tenantService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantService tenantService)
            : base(options)
        {
            _tenantService = tenantService;
        }

        public DbSet<Outlet> Outlets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Models.Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Models.Customer> Customers { get; set; }
        public DbSet<Models.RestaurantTable> RestaurantTables { get; set; }
        public DbSet<Models.Staff> StaffMembers { get; set; }
        public DbSet<Models.Expense> Expenses { get; set; }
        public DbSet<Models.Subscription> Subscriptions { get; set; }
        public DbSet<Models.AuditLog> AuditLogs { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }
        public DbSet<CreditNote> CreditNotes { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply Global Query Filter for Multi-Tenancy
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IMultiTenant).IsAssignableFrom(entityType.ClrType))
                {
                    var method = typeof(ApplicationDbContext)
                        .GetMethod(nameof(ConfigureTenantFilter), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
                        .MakeGenericMethod(entityType.ClrType);
                    method.Invoke(this, new object[] { modelBuilder });
                }
            }
        }

        private void ConfigureTenantFilter<TEntity>(ModelBuilder modelBuilder) where TEntity : class, IMultiTenant
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(
                e => _tenantService.GetRole() == "Owner" || e.OutletId == _tenantService.GetOutletId()
            );

            // Pricing precision for decimal
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetPrecision(18);
                property.SetScale(2);
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var outletId = _tenantService.GetOutletId();

            foreach (var entry in ChangeTracker.Entries<IMultiTenant>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (outletId.HasValue && entry.Entity.OutletId == 0)
                        {
                            entry.Entity.OutletId = outletId.Value;
                        }
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
