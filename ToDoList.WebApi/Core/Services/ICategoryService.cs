using ToDoList.WebApi.Core.Models.Domains;
using ToDoList.WebApi.Core.Models.Dtos;

namespace ToDoList.WebApi.Core.Services
{
    public interface ICategoryService
    {
        IEnumerable<ReadCategoryDto> Get();
        ReadCategoryDto Get(int id);
        void Update(int id, WriteCategoryDto category);
        int Add(WriteCategoryDto category);
        void Delete(int id);
    }
}
