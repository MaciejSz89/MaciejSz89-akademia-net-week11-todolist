using ToDoList.WebApi.Core;
using ToDoList.WebApi.Core.Models.Domains;
using ToDoList.WebApi.Core.Services;

namespace ToDoList.WebApi.Persistence.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Category> Get()
        {
            return _unitOfWork.Category.Get();
        }

        public Category Get(int id)
        {
            return _unitOfWork.Category.Get(id);
        }
        public void Update(Category category)
        {
            _unitOfWork.Category.Update(category);
            _unitOfWork.Complete();
        }

        public void Delete(int id)
        {
            _unitOfWork.Category.Delete(id);
            _unitOfWork.Complete();
        }

        public void Add(Category category)
        {
            _unitOfWork.Category.Add(category);
            _unitOfWork.Complete();
        }
    }
}
