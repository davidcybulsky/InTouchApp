using InTouchApi.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace InTouchApi.Application.Authorization.Handlers
{
    public class EditOrDeleteCommentReactionRequirementHandler : AuthorizationHandler<EditOrDeleteResourceRequirement, CommentReaction>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EditOrDeleteResourceRequirement requirement, CommentReaction resource)
        {
            var user = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = int.Parse(user);

            if (userId == resource.UserId)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
