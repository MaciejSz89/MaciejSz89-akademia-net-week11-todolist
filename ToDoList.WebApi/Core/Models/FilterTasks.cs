using System.ComponentModel.DataAnnotations;

namespace ToDoList.WebApi.Core.Models
{
    public class FilterTasks
    {
        public string? Title { get; set; }
        public int CategoryId { get; set; }

        public bool IsExecuted { get; set; }
    }
}