using TaskManagerSystem.Domain.Task;

namespace TaskManagerSystem.Domain.Interfaces.Services
{
    public interface ITaskService
    {
        Task<bool> InsertTask(TaskRequest task);
    }
}
