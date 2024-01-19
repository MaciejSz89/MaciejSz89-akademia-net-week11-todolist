using ToDoList.WebApi.Core.Models.Domains;

namespace ToDoList.WebApi.Core.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> Get();
        Category Get(int id);
        void Update(Category category);
        void Add(Category category);
        void Delete(int id);
    }
}
