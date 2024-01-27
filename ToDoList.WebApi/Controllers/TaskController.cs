using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.WebApi.Core.Models;
using ToDoList.WebApi.Core.Models.Dtos;
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
        public IActionResult GetTasks([FromBody] GetTasksParams param)
        {
            var tasks = _taskService.Get(param);

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public IActionResult GetTask(int id = 0)
        {
            var task = _taskService.Get(id);

            return Ok(task);
        }

        [HttpPost]
        public IActionResult AddTask([FromBody]CreateTaskDto taskDto)
        {
            var id = _taskService.Add(taskDto);

            return Created($"/api/Task/{id}", null);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateTask([FromRoute]int id, [FromBody] UpdateTaskDto taskDto)
        {
            _taskService.Update(id, taskDto);

            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id = 0)
        {
            _taskService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}/Finish")]
        public IActionResult FinishTask(int id = 0)
        {
            _taskService.Finish(id);

            return Ok();
        }

    }
}
