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
    public class UpdateStatusTaskCommandHandler : IRequestHandler<UpdateStatusTaskCommand, ApiResult<string>>
    {
        private readonly IRepository<SimpleTask> _repository;
        private readonly ILogger<UpdateStatusTaskCommandHandler> _logger;

        public UpdateStatusTaskCommandHandler(IRepository<SimpleTask> repository, IMapper mapper, ILogger<UpdateStatusTaskCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ApiResult<string>> Handle(UpdateStatusTaskCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Task a ser alterada! Request: {JsonSerializer.Serialize(request)}");

            var response = ApiResult<string>.CreateInstance();

            var entity = await _repository.GetById(request.Id);
            if (entity is null)
            {
                _logger.LogWarning($"Task com id {request.Id} não foi encontrada!");
                response.SetResultCode(HttpStatusCode.NotFound);
                return response;
            }
            entity.AlterarStatus(request.Status);

            await _repository.Update(entity);

            _logger.LogInformation($"Task alterada! Entity: {JsonSerializer.Serialize(entity)}");

            response.SetResultCode(HttpStatusCode.NoContent);
            return response;
        }
    }
}
