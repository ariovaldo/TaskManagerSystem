using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using TaskManagerSystem.Domain.Base;
using TaskManagerSystem.Domain.Interfaces.Repository;
using TaskManagerSystem.Domain.Task;

namespace TaskManagerSystem.Application.Task.Command.InsertTask
{
    public class InsertTaskCommandHandler : IRequestHandler<InsertTaskCommand, ApiResult<string>>
    {
        private readonly IRepository<SimpleTask> _repository;
        private readonly ILogger<InsertTaskCommandHandler> _logger;

        public InsertTaskCommandHandler(IRepository<SimpleTask> repository, ILogger<InsertTaskCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ApiResult<string>> Handle(InsertTaskCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Task a ser inserida! Request: {JsonSerializer.Serialize(request)}");

            var response = ApiResult<string>.CreateInstance();

            var entity = await _repository.Insert(new SimpleTask(request.Title, request.Description, request.Date));

            _logger.LogInformation($"Task criada! Entity: {JsonSerializer.Serialize(entity)}");

            if (entity != null)
                return response.SetResultCode(HttpStatusCode.Created);
            else
                return response.SetResultCode(HttpStatusCode.InternalServerError);
        }
    }
}
