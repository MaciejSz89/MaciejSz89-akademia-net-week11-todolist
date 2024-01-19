using Microsoft.EntityFrameworkCore;
using ToDoList.WebApi.Core.Models.Domains;
using Task = ToDoList.WebApi.Core.Models.Domains.Task;

namespace ToDoList.WebApi.Core
{
    public interface IToDoListContext
    {
        DbSet<Task> Tasks { get; set; }

        DbSet<Category> Categories { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }

        int SaveChanges();
    }
}
