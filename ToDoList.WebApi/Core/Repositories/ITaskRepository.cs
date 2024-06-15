using MyFinances.WebApi.Models.Domains;
using ToDoList.WebApi.Core.Models;
using ToDoList.WebApi.Core.Models.Domains;
using Task = ToDoList.WebApi.Core.Models.Domains.Task;

namespace ToDoList.WebApi.Core.Repositories
{
    public interface ITaskRepository
    {
        IDataPage<Task> Get(GetTasksParams param);
        Task Get(int id);
        void Add(Task task);
        void Update(int id, Task task);
        void Delete(int id);
        void Finish(int id);
        void Restore(int id);
    }
}
