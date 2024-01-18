using ToDoList.WebApi.Core.Models.Domains;

namespace ToDoList.WebApi.Core.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        User? Get(string email);
    }
}
