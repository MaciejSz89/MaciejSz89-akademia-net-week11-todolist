using AutoMapper;
using ToDoList.Core.Dtos;
using ToDoList.WebApi.Core;
using ToDoList.WebApi.Core.Models;
using ToDoList.WebApi.Core.Services;
using ToDoList.WebApi.Exceptions;
using Task = ToDoList.WebApi.Core.Models.Domains.Task;

namespace ToDoList.WebApi.Persistence.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TaskService> _logger;
        private readonly IUserContextService _userContextService;

        public TaskService(IUnitOfWork unitOfWork,
                           IMapper mapper,
                           ILogger<TaskService> logger,
                           IUserContextService userContextService)
        {
            _unitOfWork= unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _userContextService = userContextService;
        }

        public ReadTasksPageDto Get(GetTasksParamsDto param)
        {
            var tasksPage = _unitOfWork.Task.Get(_mapper.Map<GetTasksParams>(param));
            string message = $"Tasks with filter CategoryId:{param.CategoryId}, IsExecuted:{param.IsExecuted}, Title:{param.Title} read";
            _logger.LogInformation(message);
            return _mapper.Map<ReadTasksPageDto>(tasksPage);
        }


        public ReadTaskDto Get(int id)
        {
            var task = _mapper.Map<ReadTaskDto>(_unitOfWork.Task.Get(id));
            string message = $"Task with id {task.Id} read";
            _logger.LogInformation(message);
            return task;
        }

        public int Add(CreateTaskDto taskDto)
        {
            var task = _mapper.Map<Task>(taskDto);
            var category = _unitOfWork.Category.Get(taskDto.CategoryId);

            if (category.UserId != _userContextService.UserId)
            {
                throw new ForbidException();
            }

            _unitOfWork.Task.Add(task);
            _unitOfWork.Complete();
            _logger.LogInformation($"Task with id {task.Id} created");
            return task.Id;
        }

        public void Update(int id, UpdateTaskDto taskDto)
        {
            var taskToUpdate = _mapper.Map<Task>(taskDto);
            _unitOfWork.Task.Update(id, taskToUpdate);
            _unitOfWork.Complete();
            _logger.LogInformation($"Task with id {id} updated");
        }

        public void Delete(int id)
        {
            _unitOfWork.Task.Delete(id);
            _unitOfWork.Complete();
            string message = $"Task with id {id} deleted";
            _logger.LogInformation(message);
        }

        public void Finish(int id)
        {
            _unitOfWork.Task.Finish(id);
            _unitOfWork.Complete();
            _logger.LogInformation($"Task with id {id} finished");
        }      
        public void Restore(int id)
        {
            _unitOfWork.Task.Restore(id);
            _unitOfWork.Complete();
            _logger.LogInformation($"Task with id {id} restored");
        }

    }
}
