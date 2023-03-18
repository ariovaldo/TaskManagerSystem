﻿using System.ComponentModel.DataAnnotations;
using TaskManagerSystem.Domain.Base;

namespace TaskManagerSystem.Domain.Task
{
    public class SimpleTask : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public TaskStatusEnum Status { get; private set; }

        public SimpleTask()
        {

        }

        public SimpleTask(string title, string description, DateTime date)  
        {
            Title = title;
            Description = description;
            Date = date;
            Status= TaskStatusEnum.Todo;
        }

        public void AlterarStatus(TaskStatusEnum status)
        {
            Status= status;
        }
    }
}