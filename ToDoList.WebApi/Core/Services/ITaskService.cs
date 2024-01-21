using ToDoList.WebApi.Core.Models;
using ToDoList.WebApi.Core.Models.Dtos;

namespace ToDoList.WebApi.Core.Services
{
    public interface ITaskService
    {
        IEnumerable<ReadTaskDto> Get(GetTasksParams param);
        ReadTaskDto Get(int id);
        int Add(WriteTaskDto taskDto);
        void Update(int id, WriteTaskDto taskDto);

        void Delete(int id);
        void Finish(int id);
    }
}
