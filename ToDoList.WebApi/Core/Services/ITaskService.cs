using ToDoList.WebApi.Core.Models;
using ToDoList.WebApi.Core.Models.Dtos;

namespace ToDoList.WebApi.Core.Services
{
    public interface ITaskService
    {
        IEnumerable<ReadTaskDto> Get(GetTasksParams param);
        ReadTaskDto Get(int id);
        int Add(CreateTaskDto taskDto);
        void Update(int id, UpdateTaskDto taskDto);

        void Delete(int id);
        void Finish(int id);
    }
}
