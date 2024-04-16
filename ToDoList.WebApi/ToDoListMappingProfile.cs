using AutoMapper;
using MyFinances.WebApi.Models.Domains;
using ToDoList.Core.Dtos;
using ToDoList.WebApi.Core.Models;
using ToDoList.WebApi.Core.Models.Domains;
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

            CreateMap<GetTasksParamsDto, GetTasksParams>();

            CreateMap<GetCategoriesParamsDto, GetCategoriesParams>();

            CreateMap<IDataPage<Task>, ReadTasksPageDto>()
                .ForMember(m=>m.Tasks, c=>c.MapFrom(s=>s.Items));

            CreateMap<IDataPage<Category>, ReadCategoriesPageDto>()
                .ForMember(m => m.Categories, c => c.MapFrom(s => s.Items));

        }
    }
}
