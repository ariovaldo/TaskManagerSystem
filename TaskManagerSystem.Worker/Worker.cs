using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;
using TaskManagerSystem.Domain.Interfaces.Services;
using TaskManagerSystem.Domain.Task;
using TaskManagerSystem.Infra.Adapter.Configuration;
using TaskManagerSystem.Infra.Adapter.RabbitMQ;

namespace TaskManagerSystem.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly RabbitProvider _rabbitMqProvider;
        private readonly RabbitMQConfiguration _rabbitConfig;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _serviceScopeFactory = serviceScopeFactory;
            _rabbitConfig = _configuration.GetSection("RabbitMQConfiguration").Get<RabbitMQConfiguration>();
            _rabbitMqProvider = RabbitProvider.Build(_rabbitConfig);
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                try
                {
                    IServiceScope serviceScope = _serviceScopeFactory.CreateScope();
                    ITaskService _taskService = serviceScope.ServiceProvider.GetRequiredService<ITaskService>();
                    var channel = _rabbitMqProvider.GetChannel();

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += async (model, ea) =>
                    {
                        try
                        {
                            var body = ea.Body.ToArray();
                            var message = Encoding.UTF8.GetString(body);

                            var task = _rabbitMqProvider.ConvertByteArrayToMessage<TaskRequest>(ea.Body.ToArray());

                            if (!string.IsNullOrEmpty(task?.Title))
                               await _taskService.InsertTask(task);

                            channel.BasicAck(ea.DeliveryTag, false);
                        }
                        catch (Exception)
                        {
                            channel.BasicNack(ea.DeliveryTag, false, true);
                        }
                    };

                    channel.BasicConsume(queue: _rabbitConfig.Queue, autoAck: false, consumer: consumer);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error");
                }


                await Task.Delay(100000, stoppingToken);
            }

            _logger.LogInformation("Worker is finishing at: {time}", DateTimeOffset.Now);
        }
    }
}