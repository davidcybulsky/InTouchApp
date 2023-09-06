using InTouchApi.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace InTouchApi.Application.Authorization
{
    public class EditPostRequirementHandler : AuthorizationHandler<EditResourceRequirement, Post>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EditResourceRequirement requirement, Post resource)
        {
            var user = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = int.Parse(user);

            if (userId == resource.AuthorId)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
