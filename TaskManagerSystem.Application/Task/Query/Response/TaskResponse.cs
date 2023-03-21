using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerSystem.Domain.Task;

namespace TaskManagerSystem.Application.Task.Query.Response
{
    public class TaskResponse
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        [EnumDataType(typeof(TaskStatusEnum))]
        public TaskStatusEnum Status { get; set; }
    }
}
