﻿using ToDoList.WebApi.Core.Models.Domains;

namespace ToDoList.WebApi.Core.Models.Dtos
{
    public class ReadTaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;    
        public DateTime? Term { get; set; }
        public bool IsExecuted { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
