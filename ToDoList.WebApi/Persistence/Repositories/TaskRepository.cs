using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Authorization;
using ToDoList.WebApi.Core;
using ToDoList.WebApi.Core.Models;
using ToDoList.WebApi.Core.Models.Domains;
using ToDoList.WebApi.Core.Repositories;
using ToDoList.WebApi.Core.Services;
using ToDoList.WebApi.Exceptions;
using ToDoList.WebApi.Persistance.Services;
using Task = ToDoList.WebApi.Core.Models.Domains.Task;

namespace ToDoList.WebApi.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private IToDoListContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public TaskRepository(IToDoListContext context,
                              IAuthorizationService authorizationService,
                              IUserContextService userContextService)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public IEnumerable<Task> Get(GetTaskParams param)
        {
            var tasks = _context.Tasks
                                .Include(x => x.Category)
                                .Where(x => x.UserId == _userContextService.UserId
                                         && x.IsExecuted == param.IsExecuted);

            if (param.CategoryId != 0)
                tasks = tasks.Where(x => x.CategoryId == param.CategoryId);


            if (!string.IsNullOrWhiteSpace(param.Title))
                tasks = tasks.Where(x => x.Title.ToLower().Contains(param.Title.ToLower()));

            return tasks.OrderBy(x => x.Term).ToList();
        }


        public Task Get(int id)
        {
            var task = _context.Tasks.SingleOrDefault(x => x.Id == id);

            if (task is null)
                throw new NotFoundException("Task not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User,
                                                                           task,
                                                                           new ResourceOperationRequirement(ResourceOperation.Read)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            return task;

        }

        public void Add(Task task)
        {
            _context.Tasks.Add(task);
        }

        public void Update(Task task)
        {
            var taskToUpdate = _context.Tasks
                                       .SingleOrDefault(x => x.Id == task.Id);

            if (taskToUpdate is null)
                throw new NotFoundException("Task not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User,
                                                                           taskToUpdate,
                                                                           new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            taskToUpdate.Title = task.Title;
            taskToUpdate.Description = task.Description;
            taskToUpdate.CategoryId = task.CategoryId;
            taskToUpdate.IsExecuted = task.IsExecuted;
            taskToUpdate.Term = task.Term;

            _context.Tasks.Update(taskToUpdate);

        }

        public void Delete(int id)
        {
            var taskToDelete = _context.Tasks
                                       .SingleOrDefault(x => x.Id == id);

            if (taskToDelete is null)
                throw new NotFoundException("Task not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User,
                                                                           taskToDelete,
                                                                           new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            _context.Tasks.Remove(taskToDelete);
        }

        public void Finish(int id)
        {
            var taskToUpdate = _context.Tasks
                                       .SingleOrDefault(x => x.Id == id);

            if (taskToUpdate is null)
                throw new NotFoundException("Task not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User,
                                                                           taskToUpdate,
                                                                           new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            taskToUpdate.IsExecuted = true;

            _context.Tasks.Update(taskToUpdate);
        }
    }
}
