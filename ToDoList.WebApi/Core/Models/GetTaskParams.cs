namespace ToDoList.WebApi.Core.Models
{
    public class GetTaskParams
    {
        public int UserId { get; set; }
        public bool IsExecuted { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; } = null!;
    }
}
