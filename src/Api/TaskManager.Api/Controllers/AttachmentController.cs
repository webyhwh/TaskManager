using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;
using TaskManager.Application.Attachments.Commands.AddAttachment;
using TaskManager.Application.Attachments.Commands.DeleteAttachment;
using TaskManager.Application.Common;

namespace TaskManager.Api.Controllers
{
    /// <summary>
    /// Контроллер управления вложениями
    /// </summary>
    [Route("api/attachments")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AttachmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Добавить вложение к задаче
        /// </summary>
        /// <param name="command">Запрос на добавление вложения к задаче</param>
        [HttpPost]
        [Consumes("multipart/form-data")]
        [SwaggerResponse(200, "Вложение успешно добавлено", typeof(AddAttachmentResponse))]
        [SwaggerResponse(400, "Ошибка валидации входных параметров", typeof(InvalidRequestResponse))]
        [SwaggerResponse(500, "Неопределенная ошибка", typeof(InternalErrorResponse))]
        public async Task<AddAttachmentResponse> AddAttachment([FromForm] AddAttachmentCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Удалить вложение
        /// </summary>
        /// <param name="id">Идентификатор вложения</param>
        [HttpDelete("{id}")]
        [SwaggerResponse(204, "Вложение успешно удалено")]
        [SwaggerResponse(400, "Ошибка валидации входных параметров", typeof(InvalidRequestResponse))]
        [SwaggerResponse(500, "Неопределенная ошибка", typeof(InternalErrorResponse))]
        public async Task<IActionResult> DeleteAttachment([FromRoute, BindRequired] Guid id)
        {
            await _mediator.Send(new DeleteAttachmentCommand(id));

            return NoContent();
        }
    }
}
