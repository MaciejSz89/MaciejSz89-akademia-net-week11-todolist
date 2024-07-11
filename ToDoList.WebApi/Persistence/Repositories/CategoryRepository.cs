using Microsoft.EntityFrameworkCore;
using ToDoList.WebApi.Core;
using ToDoList.WebApi.Core.Models.Domains;
using ToDoList.WebApi.Core.Repositories;
using System.Threading.Tasks;
using ToDoList.WebApi.Exceptions;
using Microsoft.AspNetCore.Authorization;
using ToDoList.WebApi.Core.Services;
using RestaurantAPI.Authorization;
using MyFinances.WebApi.Models.Domains;
using ToDoList.WebApi.Core.Models;
using ToDoList.Core;

namespace MyTasks.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IToDoListContext _context;
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
            var categories = _context.Categories
                                   .OrderBy(x => x.Name)
                                   .Where(x => x.UserId == _userContextService.UserId)
                                   .ToList();
            return categories;
        }

        public IDataPage<Category> Get(GetCategoriesParams param)
        {
            var baseQuery = _context.Categories                
                    .Where(x => x.UserId == _userContextService.UserId);

            switch (param.SortMethod)
            {
                case CategorySortMethod.ByIdAscending:
                    baseQuery = baseQuery.OrderBy(x => x.Id);
                    break;
                case CategorySortMethod.ByIdDescending:
                    baseQuery = baseQuery.OrderByDescending(x => x.Id);
                    break;
                case CategorySortMethod.ByNameAscending:
                    baseQuery = baseQuery.OrderBy(x => x.Name);
                    break;
                case CategorySortMethod.ByNameDescending:
                    baseQuery = baseQuery.OrderByDescending(x => x.Name);
                    break;                
                default:
                    break;
            }

            var lastPage = (_context.Categories.Count() + param.PageSize - 1) / param.PageSize;
            var updatedCurrentPage = lastPage > param.PageSize * (param.PageNumber - 1)
                       ? param.PageNumber
                       : lastPage;

            var categories = baseQuery
                        .Skip(param.PageSize * (param.PageNumber - 1))
                        .Take(param.PageSize)
                        .ToList();

            var result = new DataPage<Category>
            {
                Items = categories,
                LastPage = lastPage,
                CurrentPage = updatedCurrentPage

            };

            return result;
        }

        public Category Get(int id)
        {
            var category = _context.Categories
                           .SingleOrDefault(x => x.Id == id) ?? throw new NotFoundException("Category not found");
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User,
                                                                           category,
                                                                           new ResourceOperationRequirement(ResourceOperation.Read)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            return category;
        }
        public void Update(int id, Category category)
        {
            var categoryToUpdate = _context.Categories
                                           .SingleOrDefault(x => x.Id == id) ?? throw new NotFoundException("Category not found");
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
                                           .SingleOrDefault(x => x.Id == id) ?? throw new NotFoundException("Category not found");
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User,
                                                                           categoryToDelete,
                                                                           new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            if (categoryToDelete.Tasks != null && categoryToDelete.Tasks.Any())
                throw new ReferencedToAnotherObjectException("Category is referenced to one or more task.");



            _context.Categories.Remove(categoryToDelete);
        }

        public void Add(Category category)
        {

            if (_userContextService.UserId == null)
            {
                throw new ForbidException();
            }

            category.UserId = (int)_userContextService.UserId;
            _context.Categories.Add(category);
        }
    }
}
