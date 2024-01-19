using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ToDoList.WebApi.Core.Models.Domains
{
    public partial class User
    {
        public User()
        {
            Categories = new Collection<Category>();
            Tasks = new Collection<Task>();
        }

        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public int? RoleId { get; set; }

        public Role? Role { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
