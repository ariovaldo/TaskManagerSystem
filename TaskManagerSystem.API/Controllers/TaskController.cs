using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerSystem.Application.Task.Command.InsertTask;
using TaskManagerSystem.Application.Task.Command.InsertTaskCommandMessage;
using TaskManagerSystem.Application.Task.Command.UpdateTask;
using TaskManagerSystem.Application.Task.Query.GetAllTasks;
using TaskManagerSystem.Application.Task.Query.GetTask;
using TaskManagerSystem.Application.Task.Query.Response;
using TaskManagerSystem.Domain.Base;
using TaskManagerSystem.Domain.Interfaces.Services;
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
        /// Obter as Tasks
        /// </summary>
        /// <param name="request">Filtro com status</param>
        /// <returns>Lista de tasks</returns>
        /// <response code="200">Lista de tasks</response>
        /// <response code="500">Erro interno</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResult<IEnumerable<TaskResponse>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResult<string>))]
        public async Task<IActionResult> Get([FromQuery] GetAllTasksQuery request )
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Obter determinada Task correspondente ao id
        /// </summary>
        /// <param name="id">Id da task</param>
        /// <returns>Task</returns>
        /// <response code="200">Task específica</response>
        /// <response code="404">Task não encontrada</response>
        /// <response code="500">Erro interno</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResult<TaskResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResult<string>))]
        public async Task<IActionResult> GetById(long id)
        {
            var query = new GetTaskByIdQuery() { Id = id };
            var response = await _mediator.Send(query);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Inserir uma Task diretamente no banco (Obsoleto)
        /// </summary>
        /// <param name="request">Request com os principais campos da task</param>
        /// <returns></returns>
        /// <response code="201">Task Inserida</response>
        /// <response code="500">Erro interno</response>
        [HttpPost]
        [Obsolete]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResult<string>))]
        public async Task<IActionResult> InsertTask([FromBody] InsertTaskCommand request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }


        /// <summary>
        /// Inserir a task via mensageria
        /// </summary>
        /// <param name="request">Request com os principais campos da task</param>
        /// <returns></returns>
        /// <response code="202">Task Inserida</response>
        /// <response code="500">Erro interno</response>
        [HttpPost("message")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResult<string>))]
        public async Task<IActionResult> InsertTask([FromBody] InsertTaskMessageCommand request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Alterar os dados da task
        /// </summary>
        /// <param name="id">Id da task específica</param>
        /// <param name="request">Dados da task a ser alterado</param>
        /// <returns></returns>
        /// <response code="204">Task alterada</response>
        /// <response code="404">Task não encontrada</response>
        /// <response code="500">Erro interno</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResult<string>))]
        public async Task<IActionResult> UpdateTask(long id, [FromBody] UpdateTaskCommand request)
        {
            request.Id = id;
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Alterar o status da task
        /// </summary>
        /// <param name="id">Id da task específica</param>
        /// <param name="request">Status a ser alterado</param>
        /// <returns></returns>
        /// <response code="204">Task alterada</response>
        /// <response code="404">Task não encontrada</response>
        /// <response code="500">Erro interno</response>
        [HttpPut("{id}/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResult<string>))]
        public async Task<IActionResult> UpdateStatusTask(long id, [FromBody] UpdateStatusTaskCommand request)
        {

            request.Id = id;
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }


        /// <summary>
        /// Remover determinada task
        /// </summary>
        /// <param name="id">Id da task específica</param>
        /// <returns></returns>
        /// <response code="204">Task removida</response>
        /// <response code="404">Task não encontrada</response>
        /// <response code="500">Erro interno</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResult<string>))]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _mediator.Send(new RemoveTaskCommand { Id = id});
            return StatusCode(response.StatusCode, response);
        }
    }
}
