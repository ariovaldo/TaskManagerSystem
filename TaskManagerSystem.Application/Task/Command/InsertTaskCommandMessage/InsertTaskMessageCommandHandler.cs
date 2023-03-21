using Azure;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using TaskManagerSystem.Application.Services.MessageBus;
using TaskManagerSystem.Application.Task.Command.InsertTaskCommandMessage;
using TaskManagerSystem.Domain.Base;
using TaskManagerSystem.Domain.Interfaces.MessageBus;

namespace TaskManagerSystem.Application.Task.Command.InsertTask
{
    public class InsertTaskMessageCommandHandler : IRequestHandler<InsertTaskMessageCommand, ApiResult<string>>
    {
        private readonly ISendMessage _messageService;
        private readonly ILogger<InsertTaskCommandHandler> _logger;

        public InsertTaskMessageCommandHandler(ISendMessage messageService, ILogger<InsertTaskCommandHandler> logger)
        {
            _messageService = messageService;
            _logger = logger;
        }

        public async Task<ApiResult<string>> Handle(InsertTaskMessageCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Task a ser inserida! Request: {JsonSerializer.Serialize(request)}");

            _messageService.SendMessage(request);

            return new ApiResult<string>().SetResultCode(HttpStatusCode.Accepted);
        }
    }
}
