using ToDoList.Core.Dtos;
using ToDoList.WebApi.Core.Models;

namespace ToDoList.WebApi.Core.Services
{
    public interface ITaskService
    {
        ReadTasksPageDto Get(GetTasksParamsDto param);
        ReadTaskDto Get(int id);
        int Add(CreateTaskDto taskDto);
        void Update(int id, UpdateTaskDto taskDto);

        void Delete(int id);
        void Finish(int id);
        void Restore(int id);
    }
}
