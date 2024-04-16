
using ToDoList.Core.Dtos;

namespace ToDoList.WebApi.Core.Services
{
    public interface ICategoryService
    {
        ReadCategoriesPageDto Get(GetCategoriesParamsDto param);
        ReadCategoryDto Get(int id);
        void Update(int id, WriteCategoryDto category);
        int Add(WriteCategoryDto category);
        void Delete(int id);
    }
}
