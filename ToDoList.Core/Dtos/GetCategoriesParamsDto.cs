using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.Core.Dtos
{
    public class GetCategoriesParamsDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public CategorySortMethod SortMethod { get; set; }
    }

}
