using Microsoft.AspNetCore.Authorization;

namespace InTouchApi.Application.Authorization
{
    public class CanAcceptFriendRequestRequirement : IAuthorizationRequirement
    {
        public CanAcceptFriendRequestRequirement()
        {

        }
    }
}
