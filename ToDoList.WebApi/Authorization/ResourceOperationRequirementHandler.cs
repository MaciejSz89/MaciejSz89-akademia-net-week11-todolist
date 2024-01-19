using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Authorization;
using System.Security.Claims;
using ToDoList.WebApi.Core.Models;
using ToDoList.WebApi.Core.Models.Domains;
using Task = System.Threading.Tasks.Task;

namespace ToDoList.WebApi.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, IUserDependent>
    {
        private readonly ILogger<ResourceOperationRequirementHandler> _logger;

        public ResourceOperationRequirementHandler(ILogger<ResourceOperationRequirementHandler> logger)
        {
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, IUserDependent entity)
        {
            if (requirement.ResourceOperation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }


            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var role = context.User.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;

            if (userId != null && (entity.UserId == int.Parse(userId) || role == RoleName.Admin.ToString()))
            {
                context.Succeed(requirement);
                _logger.LogInformation("Authorization succeded");
            }
            else
            {
                _logger.LogInformation("Authorization failed");
            }

            return Task.CompletedTask;
        }
    }
}
