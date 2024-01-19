using System.Security.Claims;
using ToDoList.WebApi.Core.Services;

namespace ToDoList.WebApi.Persistance.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => _httpContextAccessor.HttpContext is null ? throw new NullReferenceException() : _httpContextAccessor.HttpContext.User;

            
          

        public int? UserId
        {
            get
            {
                var nameIdentifier = User is null ? null : User.FindFirst(ClaimTypes.NameIdentifier);

                if (nameIdentifier is null)
                    return null;

                return int.Parse(nameIdentifier.Value);
            }
        }
    }
}
