using ToDoList.WebApi.Core.Models;
using ToDoList.WebApi.Core.Models.Domains;
using Task = ToDoList.WebApi.Core.Models.Domains.Task;

namespace ToDoList.WebApi.Core.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<Task> Get(GetTaskParams param);
        Task Get(int id);
        void Add(Task task);
        void Update(Task task);
        void Delete(int id);
        void Finish(int id);
    }
}
