using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Application.Common;
using TaskManager.Application.Tasks.Commands.CreateTask;
using TaskManager.Application.Tasks.Commands.DeleteTask;
using TaskManager.Application.Tasks.Commands.UpdateTask;
using TaskManager.Application.Tasks.Queries.GetTasks;

namespace TaskManager.Api.Controllers
{
    /// <summary>
    /// Контроллер управления задачами
    /// </summary>
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получить список задач
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, "Возвращены найденные задачи", typeof(IEnumerable<TaskDto>))]
        [SwaggerResponse(400, "Ошибка валидации входных параметров", typeof(InvalidRequestResponse))]
        [SwaggerResponse(500, "Неопределенная ошибка", typeof(InternalErrorResponse))]
        public async Task<IEnumerable<TaskDto>> GetTasks([FromQuery] GetTasksQuery getTasksQuery)
        {
            return await _mediator.Send(getTasksQuery);
        }

        /// <summary>
        /// Создать задачу
        /// </summary>
        /// <param name="command">Запрос на создание задачи</param>
        [HttpPost]
        [Consumes("multipart/form-data")]
        [SwaggerResponse(200, "Задача успешно создана", typeof(CreateTaskResponse))]
        [SwaggerResponse(400, "Ошибка валидации входных параметров", typeof(InvalidRequestResponse))]
        [SwaggerResponse(500, "Неопределенная ошибка", typeof(InternalErrorResponse))]
        public async Task<CreateTaskResponse> CreateTask([FromForm] CreateTaskCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Обновить задачу
        /// </summary>
        /// <param name="command">Запрос на обновление задачи</param>
        [HttpPatch]
        [SwaggerResponse(204, "Задача успешно обновлена")]
        [SwaggerResponse(400, "Ошибка валидации входных параметров", typeof(InvalidRequestResponse))]
        [SwaggerResponse(500, "Неопределенная ошибка", typeof(InternalErrorResponse))]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Удалить задачу
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        [HttpDelete("{id}")]
        [SwaggerResponse(204, "Задача успешно удалена")]
        [SwaggerResponse(400, "Ошибка валидации входных параметров", typeof(InvalidRequestResponse))]
        [SwaggerResponse(500, "Неопределенная ошибка", typeof(InternalErrorResponse))]
        public async Task<IActionResult> DeleteTask([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteTaskCommand(id));

            return NoContent();
        }
    }
}
