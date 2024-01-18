using Microsoft.EntityFrameworkCore;
using ToDoList.WebApi.Core;
using ToDoList.WebApi.Core.Models;
using ToDoList.WebApi.Core.Models.Domains;
using ToDoList.WebApi.Core.Services;
using Task = ToDoList.WebApi.Core.Models.Domains.Task;

namespace ToDoList.WebApi.Persistence.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }

        public IEnumerable<Task> Get(GetTaskParams param)
        {
            return _unitOfWork.Task.Get(param);
        }


        public Task Get(int id, int userId)
        {
            return _unitOfWork.Task.Get(id, userId);
        }

        public void Add(Task task)
        {
            _unitOfWork.Task.Add(task);
            _unitOfWork.Complete();
        }

        public void Update(Task task)
        {
            _unitOfWork.Task.Update(task);
            _unitOfWork.Complete();
        }

        public void Delete(int id, int userId)
        {
            _unitOfWork.Task.Delete(id, userId);
            _unitOfWork.Complete();
        }

        public void Finish(int id, int userId)
        {
            _unitOfWork.Task.Finish(id, userId);
            _unitOfWork.Complete();
        }

    }
}
