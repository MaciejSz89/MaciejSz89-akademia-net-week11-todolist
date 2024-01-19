using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.WebApi.Core.Models;
using ToDoList.WebApi.Core.Services;

namespace ToDoList.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private ICategoryService _categoryService;

        public TaskController(ITaskService taskService, ICategoryService categoryService)
        {
            _taskService = taskService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetTasks([FromQuery] bool? isExecuted,
                                   [FromQuery] int? categoryId,
                                   [FromQuery] string? title)
        {
            var tasks = _taskService.Get(new GetTaskParams
            {
                IsExecuted = isExecuted,
                CategoryId = categoryId,
                Title = title
            });

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public IActionResult Task(int id = 0)
        {
            throw new NotImplementedException();

        }


    }
}
