using System.Security.Claims;

namespace SarjuPos.API.Services
{
    public interface ITenantService
    {
        int? GetOutletId();
        string GetRole();
    }

    public class TenantService : ITenantService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? GetOutletId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("OutletId");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var outletId))
            {
                return outletId;
            }
            return null;
        }

        public string GetRole()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value ?? "Waiter";
        }
    }
}
