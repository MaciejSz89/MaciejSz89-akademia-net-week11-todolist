using ToDoList.WebApi.Core.Models.Domains;

namespace ToDoList.WebApi.Core.Models
{
    public interface IUserDependent
    {
        User User { get; set; }
        int UserId { get; set; }
    }
}
