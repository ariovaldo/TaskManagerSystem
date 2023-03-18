using AutoMapper;
using MediatR;
using TaskManagerSystem.Domain.Base;
using TaskManagerSystem.Domain.Interfaces.Repository;
using TaskManagerSystem.Domain.Task;

namespace TaskManagerSystem.Application.Task.Query.GetAllTasks
{
    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, ApiResult<IEnumerable<TaskResponse>>>
    {
        private readonly IRepository<SimpleTask> _repository;
        private readonly IMapper _mapper;

        public GetAllTasksQueryHandler(IRepository<SimpleTask> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResult<IEnumerable<TaskResponse>>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            var response = ApiResult<IEnumerable<TaskResponse>>.CreateInstance();

            if (request != null && request.Status is TaskStatusEnum)
                return response.SetData(await Get((TaskStatusEnum)request.Status));
            else
                return response.SetData(await GetAll());

        }

        private async Task<IEnumerable<TaskResponse>> GetAll()
        {
            var tasks = await _repository.GetAll();
            return _mapper.Map<IEnumerable<TaskResponse>>(tasks);
        }

        private async Task<IEnumerable<TaskResponse>> Get(TaskStatusEnum status)
        {
            var tasks = await _repository.Get(x => x.Status.Equals(status));
            return _mapper.Map<IEnumerable<TaskResponse>>(tasks);
        }
    }
}
