using ToDoList.WebApi.Core.Models.Domains;

namespace ToDoList.WebApi.Core.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> Get(int userId);
        Category Get(int id, int userId);
        void Update(Category category);
        void Add(Category category);
        void Delete(int id, int userId);
    }
}
