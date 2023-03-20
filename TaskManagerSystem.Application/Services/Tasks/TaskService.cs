using Microsoft.Extensions.Logging;
using System.Text.Json;
using TaskManagerSystem.Domain.Interfaces.Repository;
using TaskManagerSystem.Domain.Interfaces.Services;
using TaskManagerSystem.Domain.Task;

namespace TaskManagerSystem.Application.Services.Tasks
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<SimpleTask> _repository;
        private readonly ILogger<TaskService> _logger;

        public TaskService(IRepository<SimpleTask> repository, ILogger<TaskService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<bool> InsertTask(TaskRequest task)
        {
            var entity = await _repository.Insert(new SimpleTask(task.Title, task.Description, task.Date));
            _logger.LogInformation($"Task criada! Entity: {JsonSerializer.Serialize(entity)}");

            return (entity != null);
        }
    }
}
