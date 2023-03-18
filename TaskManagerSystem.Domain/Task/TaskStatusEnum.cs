using System.ComponentModel;

namespace TaskManagerSystem.Domain.Task
{
    public enum TaskStatusEnum
    {
        [Description("Todo")]
        Todo = 1,
        [Description("Doing")]
        Doing =2,
        [Description("Done")]
        Done = 3,
        [Description("Blocked")]
        Blocked = 4
    }
}