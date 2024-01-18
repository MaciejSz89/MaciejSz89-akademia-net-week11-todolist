using Microsoft.EntityFrameworkCore;
using ToDoList.WebApi.Core;
using ToDoList.WebApi.Core.Models;
using ToDoList.WebApi.Core.Models.Domains;
using ToDoList.WebApi.Core.Repositories;
using Task = ToDoList.WebApi.Core.Models.Domains.Task;

namespace ToDoList.WebApi.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private IToDoListContext _context;

        public TaskRepository(IToDoListContext context)
        {
            _context = context;
        }

        public IEnumerable<Task> Get(GetTaskParams param)
        {
            var tasks = _context.Tasks
                                .Include(x => x.Category)
                                .Where(x => x.UserId == param.UserId
                                    && x.IsExecuted == param.IsExecuted);

            if (param.CategoryId != 0)
                tasks = tasks.Where(x => x.CategoryId == param.CategoryId);


            if (!string.IsNullOrWhiteSpace(param.Title))
                tasks = tasks.Where(x => x.Title.Contains(param.Title));

            return tasks.OrderBy(x => x.Term).ToList();
        }


        public Task Get(int id, int userId)
        {
            var task = _context.Tasks.Single(x => x.Id == id
                                               && x.UserId == userId);

            return task;

        }

        public void Add(Task task)
        {
            _context.Tasks.Add(task);
        }

        public void Update(Task task)
        {
            var taskToUpdate = _context.Tasks
                                       .Single(x => x.UserId == task.UserId
                                                 && x.Id == task.Id);

            taskToUpdate.Title = task.Title;
            taskToUpdate.Description = task.Description;
            taskToUpdate.CategoryId = task.CategoryId;
            taskToUpdate.IsExecuted = task.IsExecuted;
            taskToUpdate.Term = task.Term;

            _context.Tasks.Update(taskToUpdate);

        }

        public void Delete(int id, int userId)
        {
            var taskToDelete = _context.Tasks
                                       .Single(x => x.UserId == userId
                                                 && x.Id == id);

            _context.Tasks.Remove(taskToDelete);
        }

        public void Finish(int id, int userId)
        {
            var taskToUpdate = _context.Tasks
                                       .Single(x => x.UserId == userId
                                                 && x.Id == id);

            taskToUpdate.IsExecuted = true;

            _context.Tasks.Update(taskToUpdate);
        }
    }
}
