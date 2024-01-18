using ToDoList.WebApi.Core.Repositories;

namespace ToDoList.WebApi.Core
{
    public interface IUnitOfWork
    {
        ITaskRepository Task { get; set; }
        ICategoryRepository Category { get; set; }
        IUserRepository User { get; set; }
        void Complete();
    }
}
