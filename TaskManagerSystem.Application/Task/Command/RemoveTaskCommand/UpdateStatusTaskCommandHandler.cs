using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using TaskManagerSystem.Application.Task.Command.UpdateTask;
using TaskManagerSystem.Domain.Base;
using TaskManagerSystem.Domain.Interfaces.Repository;
using TaskManagerSystem.Domain.Task;

namespace TaskManagerSystem.Application.Task.Command.InsertTask
{
    public class RemoveTaskCommandHandler : IRequestHandler<RemoveTaskCommand, ApiResult<string>>
    {
        private readonly IRepository<SimpleTask> _repository;
        private readonly ILogger<RemoveTaskCommandHandler> _logger;

        public RemoveTaskCommandHandler(IRepository<SimpleTask> repository, IMapper mapper, ILogger<RemoveTaskCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ApiResult<string>> Handle(RemoveTaskCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Task a ser removdia! Request: {JsonSerializer.Serialize(request)}");

            var response = ApiResult<string>.CreateInstance();

            var entity = await _repository.GetById(request.Id);
            if (entity is null)
            {
                _logger.LogWarning($"Task com id {request.Id} não foi encontrada!");
                response.SetResultCode(HttpStatusCode.NotFound);
                return response;
            }
            
            await _repository.Delete(request.Id);

            _logger.LogInformation($"Task removida! Entity: {JsonSerializer.Serialize(entity)}");

            response.SetResultCode(HttpStatusCode.NoContent);
            return response;
        }
    }
}
