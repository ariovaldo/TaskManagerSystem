using MediatR;
using TaskManagerSystem.Domain.Task;
using TaskManagerSystem.Domain.Base;

namespace TaskManagerSystem.Application.Task.Command.InsertTaskCommandMessage
{
    public class InsertTaskMessageCommand : TaskRequest, IRequest<ApiResult<string>>
    {

    }
}
