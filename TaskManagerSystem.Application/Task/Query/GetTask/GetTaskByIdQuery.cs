using MediatR;
using TaskManagerSystem.Application.Task.Query.Response;
using TaskManagerSystem.Domain.Base;

namespace TaskManagerSystem.Application.Task.Query.GetTask
{
    public class GetTaskByIdQuery : IRequest<ApiResult<TaskResponse>>
    {
        public long Id { get; set; }
    }
}
