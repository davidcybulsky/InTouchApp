using System.Security.Claims;

namespace InTouchApi.Application.Interfaces
{
    public interface IUserHttpContextService
    {
        public ClaimsPrincipal? User { get; }

        public int? Id { get; }
    }
}
