using MyFinances.WebApi.Models.Domains;
using ToDoList.WebApi.Core.Models;
using ToDoList.WebApi.Core.Models.Domains;

namespace ToDoList.WebApi.Core.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Get();
        IDataPage<Category> Get(GetCategoriesParams param);
        Category Get(int id);
        void Update(int id, Category category);
        void Add(Category category);
        void Delete(int id);
    }
}
