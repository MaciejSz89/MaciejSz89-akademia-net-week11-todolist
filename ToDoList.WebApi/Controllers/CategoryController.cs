using Microsoft.AspNetCore.Mvc;
using ToDoList.WebApi.Core.Services;

namespace ToDoList.WebApi.Controllers
{
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
    }
}
