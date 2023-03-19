using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerSystem.Domain.Interfaces.MessageBus;
using TaskManagerSystem.Infra.Adapter.Configuration;
using TaskManagerSystem.Infra.Adapter.RabbitMQ;

namespace TaskManagerSystem.Application.Services.MessageBus
{
    

    public class ReceiveMessageService : IReceiveMessage
    {
        private readonly RabbitProvider _rabbitMqProvider;
        private readonly RabbitMQConfiguration _rabbitConfig;

        public ReceiveMessageService(IOptions<RabbitMQConfiguration> rabbitConfig)
        {
            _rabbitConfig = rabbitConfig.Value;
            _rabbitMqProvider = RabbitProvider.Build(_rabbitConfig);
        }

        public void ReceiveMessage<T>(T message)
        {
            //_rabbitMqProvider.Publish(message);
        }
    }
}
