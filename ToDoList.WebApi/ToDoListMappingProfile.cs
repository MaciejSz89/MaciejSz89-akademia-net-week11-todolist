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
            CreateMap<Task, TaskDto>()
                .ForMember(m => m.CategoryName, c => c.MapFrom(s => s.Category.Name));

        }
    }
}
