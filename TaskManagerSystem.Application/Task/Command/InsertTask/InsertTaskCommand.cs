using MediatR;
using TaskManagerSystem.Domain.Base;
using TaskManagerSystem.Domain.Task;

namespace TaskManagerSystem.Application.Task.Command.InsertTask
{
    public class InsertTaskCommand : TaskRequest, IRequest<ApiResult<string>>
    {
       
    }
}
