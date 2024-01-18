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

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
