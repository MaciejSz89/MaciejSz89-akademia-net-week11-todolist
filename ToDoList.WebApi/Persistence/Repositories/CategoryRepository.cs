using Microsoft.EntityFrameworkCore;
using ToDoList.WebApi.Core;
using ToDoList.WebApi.Core.Models.Domains;
using ToDoList.WebApi.Core.Repositories;
using System.Threading.Tasks;
using ToDoList.WebApi.Persistence.Exceptions;

namespace MyTasks.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private IToDoListContext _context;
        public CategoryRepository(IToDoListContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> Get(int userId)
        {
            var categories = _context.Categories
                                     .Where(x => x.UserId == userId);

            return _context.Categories
                           .Where(x => x.UserId == userId)
                           .OrderBy(x => x.Name)
                           .ToList();
        }

        public Category Get(int id, int userId)
        {
            return _context.Categories
                           .Single(x => x.UserId == userId
                                     && x.Id == id);
        }
        public void Update(Category category)
        {
            var categoryToUpdate = _context.Categories
                                           .Single(x => x.UserId == category.UserId
                                                     && x.Id == category.Id);
            categoryToUpdate.Name = category.Name;

            _context.Categories.Update(categoryToUpdate);
        }

        public void Delete(int id, int userId)
        {
            var categoryToDelete = _context.Categories
                                           .Include(x => x.Tasks)
                                           .Single(x => x.UserId == userId
                                                     && x.Id == id);

            if (categoryToDelete.Tasks != null && categoryToDelete.Tasks.Any())
                throw new ReferencedToAnotherObjectException();

            _context.Categories.Remove(categoryToDelete);
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }
    }
}
