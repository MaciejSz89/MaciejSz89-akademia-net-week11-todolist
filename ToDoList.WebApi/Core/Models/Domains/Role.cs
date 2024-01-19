using System.Collections.ObjectModel;

namespace ToDoList.WebApi.Core.Models.Domains
{
    public enum RoleName
    {
        User = 1,
        Admin
    }

    public class Role
    {
        public Role()
        {
            Users = new Collection<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<User>? Users { get; set; }

        
    }
}