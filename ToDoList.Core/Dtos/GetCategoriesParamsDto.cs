using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.Core.Dtos
{
    public class GetTasksParamsDto
    {
        public bool? IsExecuted { get; set; }
        public int? CategoryId { get; set; }
        public string Title { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
