using MyTasks.Persistence.Repositories;
using ToDoList.WebApi.Core;
using ToDoList.WebApi.Core.Repositories;
using ToDoList.WebApi.Persistence.Repositories;

namespace ToDoList.WebApi.Persisntence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IToDoListContext _context;
        public UnitOfWork(IToDoListContext context)
        {
            _context = context;
            Task = new TaskRepository(context);
            Category = new CategoryRepository(context);
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
