namespace ToDoList.WebApi.Core.Models
{
    public class GetTasksParams
    {
        public bool? IsExecuted { get; set; }
        public int? CategoryId { get; set; }
        public string? Title { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
