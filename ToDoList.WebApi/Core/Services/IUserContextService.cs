using System.Security.Claims;

namespace ToDoList.WebApi.Core.Services
{
    public interface IUserContextService
    {
        int? UserId { get; }
        ClaimsPrincipal User { get; }
    }
}