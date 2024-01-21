using Microsoft.AspNetCore.Mvc;
using ToDoList.WebApi.Core.Models.Dtos;
using ToDoList.WebApi.Core.Models;
using ToDoList.WebApi.Core.Services;
using ToDoList.WebApi.Persistence.Services;

namespace ToDoList.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _categoryService.Get();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id = 0)
        {
            var task = _categoryService.Get(id);

            return Ok(task);
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody] WriteCategoryDto taskDto)
        {
            var id = _categoryService.Add(taskDto);

            return Created($"/api/[controller]/{id}", null);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateCategory([FromRoute] int id, [FromBody] WriteCategoryDto taskDto)
        {
            _categoryService.Update(id, taskDto);

            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id = 0)
        {
            _categoryService.Delete(id);

            return NoContent();
        }
    }
}
