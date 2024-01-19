using Microsoft.AspNetCore.Authorization;
using MyTasks.Persistence.Repositories;
using ToDoList.WebApi.Core;
using ToDoList.WebApi.Core.Repositories;
using ToDoList.WebApi.Core.Services;
using ToDoList.WebApi.Persistence.Repositories;

namespace ToDoList.WebApi.Persisntence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IToDoListContext _context;
        public UnitOfWork(IToDoListContext context,
                          IAuthorizationService authorizationService,
                          IUserContextService userContextService)
        {
            _context = context;
            Task = new TaskRepository(context, authorizationService, userContextService);
            Category = new CategoryRepository(context, authorizationService, userContextService);
            User = new UserRepository(context);
        }

        public ITaskRepository Task { get; set; }
        public ICategoryRepository Category { get; set; }
        public IUserRepository User { get; set; }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
