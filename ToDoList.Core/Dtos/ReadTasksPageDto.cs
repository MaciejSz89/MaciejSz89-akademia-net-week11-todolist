using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.Core.Dtos
{
    public class ReadTasksPageDto
    {
        public IEnumerable<ReadTaskDto> Tasks { get; set; }
        public int CurrentPage { get; set; }
        public int LastPage { get; set; }
    }
}
