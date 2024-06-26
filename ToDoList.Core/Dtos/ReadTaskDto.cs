﻿using System;

namespace ToDoList.Core.Dtos
{
    public class ReadTaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public DateTime? Term { get; set; }
        public bool IsExecuted { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
