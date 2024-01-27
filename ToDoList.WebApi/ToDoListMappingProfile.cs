using AutoMapper;
using ToDoList.WebApi.Core.Models.Domains;
using ToDoList.WebApi.Core.Models.Dtos;
using Task = ToDoList.WebApi.Core.Models.Domains.Task;


namespace ToDoList.WebApi
{
    public class ToDoListMappingProfile : Profile
    {

        public ToDoListMappingProfile()
        {
            CreateMap<Task, ReadTaskDto>()
                .ForMember(m => m.CategoryName, c => c.MapFrom(s => s.Category.Name));

            CreateMap<CreateTaskDto, Task>();

            CreateMap<UpdateTaskDto, Task>();

            CreateMap<Category, ReadCategoryDto>();

            CreateMap<WriteCategoryDto, Category>();

        }
    }
}
