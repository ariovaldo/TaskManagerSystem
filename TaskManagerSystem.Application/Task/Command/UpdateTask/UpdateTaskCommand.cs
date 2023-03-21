using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TaskManagerSystem.Domain.Base;
using TaskManagerSystem.Domain.Task;

namespace TaskManagerSystem.Application.Task.Command.UpdateTask
{
    public class UpdateTaskCommand : IRequest<ApiResult<string>>
    {
        [JsonIgnore]
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public TaskStatusEnum Status { get; set; }
    }
}
