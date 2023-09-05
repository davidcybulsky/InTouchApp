using InTouchApi.Application.Interfaces;
using System.Security.Claims;

namespace InTouchApi.Infrastructure.Services
{
    public class UserHttpContextService : IUserHttpContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserHttpContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? Id => int.Parse(_httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public ClaimsPrincipal? User => _httpContextAccessor?.HttpContext?.User;
    }
}
