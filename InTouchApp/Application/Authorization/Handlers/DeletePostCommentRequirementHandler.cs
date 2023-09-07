using InTouchApi.Domain.Constants;
using InTouchApi.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace InTouchApi.Application.Authorization.Handlers
{
    public class DeletePostCommentRequirementHandler : AuthorizationHandler<DeleteResourceRequirement, PostComment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DeleteResourceRequirement requirement, PostComment resource)
        {
            var user = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = int.Parse(user);

            if (userId == resource.UserId)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var userRole = context.User.FindFirstValue(ClaimTypes.Role);

            if (userRole == ROLES.ADMIN)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
