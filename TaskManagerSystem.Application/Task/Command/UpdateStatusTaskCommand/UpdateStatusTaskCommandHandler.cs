using AutoMapper;
using Azure.Core;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Net;
using TaskManagerSystem.Application.Task.Command.UpdateTask;
using TaskManagerSystem.Domain.Base;
using TaskManagerSystem.Domain.Interfaces.Repository;
using TaskManagerSystem.Domain.Task;

namespace TaskManagerSystem.Application.Task.Command.InsertTask
{
    public class UpdateStatusTaskCommandHandler : IRequestHandler<UpdateStatusTaskCommand, ApiResult<string>>
    {
        private readonly IRepository<SimpleTask> _repository;

        public UpdateStatusTaskCommandHandler(IRepository<SimpleTask> repository, IMapper mapper)
        {
            _repository = repository;
        }

        public async Task<ApiResult<string>> Handle(UpdateStatusTaskCommand request, CancellationToken cancellationToken)
        {

            var response = ApiResult<string>.CreateInstance();

            var entity = await _repository.GetById(request.Id);
            if (entity is null)
            {
                response.SetResultCode(HttpStatusCode.NotFound);
                return response;
            }
            entity.AlterarStatus(request.Status);

            await _repository.Update(entity);

            response.SetResultCode(HttpStatusCode.NoContent);
            return response;
        }
    }
}
