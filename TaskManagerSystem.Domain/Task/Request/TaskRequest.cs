using System.ComponentModel.DataAnnotations;
using TaskManagerSystem.Domain.Base;

namespace TaskManagerSystem.Domain.Task
{
    public class TaskRequest
    {
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}