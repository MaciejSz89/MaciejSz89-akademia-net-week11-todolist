using ToDoList.Core;

namespace ToDoList.WebApi.Core.Models
{
    public class GetCategoriesParams
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public CategorySortMethod SortMethod { get; set; }

    }
}
