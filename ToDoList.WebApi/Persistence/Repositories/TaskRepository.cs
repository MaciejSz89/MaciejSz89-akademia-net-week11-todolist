using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MyFinances.WebApi.Models.Domains;
using RestaurantAPI.Authorization;
using ToDoList.WebApi.Core;
using ToDoList.WebApi.Core.Models;
using ToDoList.WebApi.Core.Models.Domains;
using ToDoList.WebApi.Core.Repositories;
using ToDoList.WebApi.Core.Services;
using ToDoList.WebApi.Exceptions;
using ToDoList.WebApi.Persistance.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Task = ToDoList.WebApi.Core.Models.Domains.Task;

namespace ToDoList.WebApi.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IToDoListContext _context;
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

        public IDataPage<Task> Get(GetTasksParams param)
        {
            var baseQuery = _context.Tasks
                                .Include(x => x.Category)
                                .Where(x => x.UserId == _userContextService.UserId);

            if (param.IsExecuted != null)
                baseQuery = baseQuery.Where(x => x.IsExecuted == param.IsExecuted);

            if (param.CategoryId != null && param.CategoryId != 0)
                baseQuery = baseQuery.Where(x => x.CategoryId == param.CategoryId);


            if (!string.IsNullOrWhiteSpace(param.Title))
                baseQuery = baseQuery.Where(x => x.Title.ToLower().Contains(param.Title.ToLower()));

            baseQuery = baseQuery.OrderBy(x => x.Term);

            var lastPage = (_context.Tasks.Count() + param.PageSize - 1) / param.PageSize;
            var updatedCurrentPage = lastPage > param.PageSize * (param.PageNumber - 1)
                       ? param.PageNumber
                       : lastPage;

            var tasks = baseQuery
                    .Skip(param.PageSize * (param.PageNumber - 1))
                    .Take(param.PageSize)
                    .ToList();

            var result = new DataPage<Task>
            {
                Items = tasks,
                LastPage = lastPage,
                CurrentPage = updatedCurrentPage

            };

            return result;
        }


        public Task Get(int id)
        {
            var task = _context.Tasks
                .Include(t=>t.Category)
                .SingleOrDefault(x => x.Id == id) ?? throw new NotFoundException("Task not found");
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

            if (_userContextService.UserId == null)
            {
                throw new ForbidException();
            }


            task.UserId = (int)_userContextService.UserId;
            _context.Tasks.Add(task);
        }

        public void Update(int id, Task task)
        {
            var taskToUpdate = _context.Tasks
                                       .SingleOrDefault(x => x.Id == id) ?? throw new NotFoundException("Task not found");
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
                                       .SingleOrDefault(x => x.Id == id) ?? throw new NotFoundException("Task not found");
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
                                       .SingleOrDefault(x => x.Id == id) ?? throw new NotFoundException("Task not found");
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
        public void Restore(int id)
        {
            var taskToUpdate = _context.Tasks
                                       .SingleOrDefault(x => x.Id == id) ?? throw new NotFoundException("Task not found");
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User,
                                                                           taskToUpdate,
                                                                           new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            taskToUpdate.IsExecuted = false;

            _context.Tasks.Update(taskToUpdate);
        }
    }
}
