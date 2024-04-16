using System;

namespace ToDoList.Core.Dtos
{
    public class CreateTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }    
        public DateTime? Term { get; set; }
        public int CategoryId { get; set; }
    }
}
