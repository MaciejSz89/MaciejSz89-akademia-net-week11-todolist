using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.Core.Dtos
{
    public class ReadCategoriesPageDto
    {
        public IEnumerable<ReadCategoryDto> Categories { get; set; }
        public int CurrentPage { get; set; }
        public int LastPage { get; set; }
    }
}
