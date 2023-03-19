using AutoMapper;
using MediatR;
using System.Net;
using TaskManagerSystem.Application.Task.Query.Response;
using TaskManagerSystem.Domain.Base;
using TaskManagerSystem.Domain.Interfaces.Repository;
using TaskManagerSystem.Domain.Task;

namespace TaskManagerSystem.Application.Task.Query.GetTask
{
    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, ApiResult<TaskResponse>>
    {
        private readonly IRepository<SimpleTask> _repository;
        private readonly IMapper _mapper;

        public GetTaskByIdQueryHandler(IRepository<SimpleTask> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResult<TaskResponse>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var response = ApiResult<TaskResponse>.CreateInstance();

            var entity = await _repository.GetById(request.Id);
            if (entity is null)
            {
                response.SetResultCode(HttpStatusCode.NotFound);
                return response;
            }

            var taskResponse = _mapper.Map<TaskResponse>(entity);

            return response.SetData(taskResponse);
        }
    }
}
