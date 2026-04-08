namespace SarjuPos.API.Models
{
    public interface IMultiTenant
    {
        int OutletId { get; set; }
    }

    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public abstract class BaseTenantEntity : BaseEntity, IMultiTenant
    {
        public int OutletId { get; set; }
    }
}
