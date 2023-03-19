using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerSystem.Application.Task.Command.InsertTask;
using TaskManagerSystem.Application.Task.Command.UpdateTask;
using TaskManagerSystem.Application.Task.Query.GetAllTasks;
using TaskManagerSystem.Application.Task.Query.GetTask;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TaskManagerSystem.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllTasksQuery request )
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var query = new GetTaskByIdQuery() { Id = id };
            var response = await _mediator.Send(query);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> InsertTask([FromBody] InsertTaskCommand request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(long id, [FromBody] UpdateTaskCommand request)
        {
            request.Id = id;
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatusTask(long id, [FromBody] UpdateStatusTaskCommand request)
        {

            request.Id = id;
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

    }
}
