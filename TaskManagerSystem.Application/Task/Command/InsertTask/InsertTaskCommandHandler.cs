using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Net;
using TaskManagerSystem.Domain.Base;
using TaskManagerSystem.Domain.Interfaces.Repository;
using TaskManagerSystem.Domain.Task;

namespace TaskManagerSystem.Application.Task.Command.InsertTask
{
    public class InsertTaskCommandHandler : IRequestHandler<InsertTaskCommand, ApiResult<string>>
    {
        private readonly IRepository<SimpleTask> _repository;

        public InsertTaskCommandHandler(IRepository<SimpleTask> repository)
        {
            _repository = repository;
        }

        public async Task<ApiResult<string>> Handle(InsertTaskCommand request, CancellationToken cancellationToken)
        {
            var response = ApiResult<string>.CreateInstance();

            var entity = await _repository.Insert(new SimpleTask(request.Title, request.Description, request.Date));

            if (entity != null)
                return response.SetResultCode(HttpStatusCode.Created);
            else
                return response.SetResultCode(HttpStatusCode.InternalServerError);
        }
    }
}
