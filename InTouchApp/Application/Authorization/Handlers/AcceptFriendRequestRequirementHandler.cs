﻿using InTouchApi.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace InTouchApi.Application.Authorization
{
    public class AcceptFriendRequestRequirementHandler : AuthorizationHandler<AcceptFriendRequestRequirement, Friendship>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AcceptFriendRequestRequirement requirement, Friendship resource)
        {
            var user = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = int.Parse(user);

            if (userId != resource.SendById && (userId == resource.UserId || userId == resource.FriendId))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
