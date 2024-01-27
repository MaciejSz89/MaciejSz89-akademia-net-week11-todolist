using ToDoList.WebApi.Core.Models.Domains;

namespace ToDoList.WebApi.Core.Models.Dtos
{
    public class UpdateTaskDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;    
        public DateTime? Term { get; set; }
        public bool IsExecuted { get; set; }
        public int CategoryId { get; set; }
    }
}
