using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ToDoList.WebApi.Core.Models.Domains
{
    public partial class Category : IUserDependent
    {
        public Category()
        {
            Tasks = new Collection<Task>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
