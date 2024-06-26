﻿using AutoMapper;
using ToDoList.Core.Dtos;
using ToDoList.WebApi.Core;
using ToDoList.WebApi.Core.Models;
using ToDoList.WebApi.Core.Models.Domains;
using ToDoList.WebApi.Core.Services;

namespace ToDoList.WebApi.Persistence.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork,
                               IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<ReadCategoryDto> Get()
        {
            var categories = _unitOfWork.Category.Get();
            return _mapper.Map<IEnumerable<ReadCategoryDto>>(categories);
        }


        public ReadCategoriesPageDto Get(GetCategoriesParamsDto param)
        {
            var categoriesPage = _unitOfWork.Category.Get(_mapper.Map<GetCategoriesParams>(param));
            return _mapper.Map<ReadCategoriesPageDto>(categoriesPage);
        }

        public ReadCategoryDto Get(int id)
        {
            var category = _unitOfWork.Category.Get(id);
            return _mapper.Map<ReadCategoryDto>(category);
        }
        public void Update(int id, WriteCategoryDto categoryDto)
        {
            _unitOfWork.Category.Update(id, _mapper.Map<Category>(categoryDto));
            _unitOfWork.Complete();
        }

        public void Delete(int id)
        {
            _unitOfWork.Category.Delete(id);
            _unitOfWork.Complete();
        }

        public int Add(WriteCategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            _unitOfWork.Category.Add(category);
            _unitOfWork.Complete();
            return category.Id;
        }

    }
}
