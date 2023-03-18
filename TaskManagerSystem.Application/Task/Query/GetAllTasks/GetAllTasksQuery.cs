using MediatR;
using TaskManagerSystem.Domain.Base;
using TaskManagerSystem.Domain.Task;

namespace TaskManagerSystem.Application.Task.Query.GetAllTasks
{
    public class GetAllTasksQuery : IRequest<ApiResult<IEnumerable<TaskResponse>>>
    {
        public TaskStatusEnum? Status { get; set; }
    }
}
