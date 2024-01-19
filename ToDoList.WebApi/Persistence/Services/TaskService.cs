using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ToDoList.WebApi.Core;
using ToDoList.WebApi.Core.Models;
using ToDoList.WebApi.Core.Models.Domains;
using ToDoList.WebApi.Core.Services;
using ToDoList.WebApi.Persistance.Services;
using Task = ToDoList.WebApi.Core.Models.Domains.Task;

namespace ToDoList.WebApi.Persistence.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TaskService> _logger;

        public TaskService(IUnitOfWork unitOfWork, 
                           ILogger<TaskService> logger)
        {
            _unitOfWork= unitOfWork;
            _logger = logger;
        }

        public IEnumerable<Task> Get(GetTaskParams param)
        {
            var tasks = _unitOfWork.Task.Get(param);
            _logger.LogInformation($"Tasks with filter CategoryId:{param.CategoryId}, IsExecuted:{param.IsExecuted}, Title:{param.Title} read");
            return tasks;
        }


        public Task Get(int id, int userId)
        {
            var task = _unitOfWork.Task.Get(id);
            _logger.LogInformation($"Task with id {task.Id} read");
            return task;
        }

        public void Add(Task task)
        {
            _unitOfWork.Task.Add(task);
            _unitOfWork.Complete();
            _logger.LogInformation($"Task with id {task.Id} deleted");
        }

        public void Update(Task task)
        {
            _unitOfWork.Task.Update(task);
            _unitOfWork.Complete();
            _logger.LogInformation($"Task with id {task.Id} updated");
        }

        public void Delete(int id)
        {
            _unitOfWork.Task.Delete(id);
            _unitOfWork.Complete();
            _logger.LogInformation($"Task with id {id} deleted");
        }

        public void Finish(int id)
        {
            _unitOfWork.Task.Finish(id);
            _unitOfWork.Complete();
            _logger.LogInformation($"Task with id {id} finished");
        }

    }
}
