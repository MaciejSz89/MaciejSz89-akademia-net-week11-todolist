using Microsoft.EntityFrameworkCore;
using ToDoList.WebApi.Core;
using ToDoList.WebApi.Core.Models.Domains;
using ToDoList.WebApi.Core.Repositories;

namespace ToDoList.WebApi.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {

        private IToDoListContext _context;

        public UserRepository(IToDoListContext context)
        {
            _context = context; 
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
        }

        public User? Get(string email)
        {
            return _context.Users
                           .Include(u=>u.Role)
                           .FirstOrDefault(u => u.Email == email);
        }
    }
}
