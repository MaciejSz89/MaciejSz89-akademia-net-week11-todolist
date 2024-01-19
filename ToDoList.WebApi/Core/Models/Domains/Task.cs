using System;
using System.Collections.Generic;

namespace ToDoList.WebApi.Core.Models.Domains
{


    public partial class Task : IUserDependent
    {

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CategoryId { get; set; }
        public DateTime? Term { get; set; }
        public bool IsExecuted { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }
}
