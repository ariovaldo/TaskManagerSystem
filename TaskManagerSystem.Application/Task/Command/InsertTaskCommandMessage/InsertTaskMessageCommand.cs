using MediatR;
using System.ComponentModel.DataAnnotations;
using TaskManagerSystem.Domain.Base;

namespace TaskManagerSystem.Application.Task.Command.InsertTaskCommandMessage
{
    public class InsertTaskMessageCommand : IRequest<ApiResult<string>>
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
