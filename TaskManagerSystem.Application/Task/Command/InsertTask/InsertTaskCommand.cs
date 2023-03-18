using MediatR;
using System.ComponentModel.DataAnnotations;
using TaskManagerSystem.Domain.Base;

namespace TaskManagerSystem.Application.Task.Command.InsertTask
{
    public class InsertTaskCommand : IRequest<ApiResult<string>>
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
