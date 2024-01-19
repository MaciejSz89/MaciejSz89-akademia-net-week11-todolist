using Microsoft.EntityFrameworkCore;
using ToDoList.WebApi.Core;
using ToDoList.WebApi.Core.Models.Domains;
using ToDoList.WebApi.Core.Repositories;
using System.Threading.Tasks;
using ToDoList.WebApi.Exceptions;
using Microsoft.AspNetCore.Authorization;
using ToDoList.WebApi.Core.Services;
using RestaurantAPI.Authorization;

namespace MyTasks.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private IToDoListContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;
        public CategoryRepository(IToDoListContext context,
                                  IAuthorizationService authorizationService,
                                  IUserContextService userContextService)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }
        public IEnumerable<Category> Get()
        {

            return _context.Categories
                           .Where(x => x.UserId == _userContextService.UserId)
                           .OrderBy(x => x.Name)
                           .ToList();
        }

        public Category Get(int id)
        {
            var category = _context.Categories
                           .SingleOrDefault(x => x.Id == id);

            if (category is null)
                throw new NotFoundException("Category not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User,
                                                                           category,
                                                                           new ResourceOperationRequirement(ResourceOperation.Read)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            return category;
        }
        public void Update(Category category)
        {
            var categoryToUpdate = _context.Categories
                                           .SingleOrDefault(x => x.Id == category.Id);

            if (categoryToUpdate is null)
                throw new NotFoundException("Category not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User,
                                                                           categoryToUpdate,
                                                                           new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            categoryToUpdate.Name = category.Name;

            _context.Categories.Update(categoryToUpdate);
        }

        public void Delete(int id)
        {
            var categoryToDelete = _context.Categories
                                           .Include(x => x.Tasks)
                                           .Single(x => x.Id == id);

            if (categoryToDelete is null)
                throw new NotFoundException("Category not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User,
                                                                           categoryToDelete,
                                                                           new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

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
