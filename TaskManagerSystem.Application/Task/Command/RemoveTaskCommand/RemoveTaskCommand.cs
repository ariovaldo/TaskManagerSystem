﻿using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TaskManagerSystem.Domain.Base;
using TaskManagerSystem.Domain.Task;

namespace TaskManagerSystem.Application.Task.Command.UpdateTask
{
    public class RemoveTaskCommand : IRequest<ApiResult<string>>
    {
        [JsonIgnore]
        public long Id { get; set; }
    }
}
