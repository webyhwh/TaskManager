using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Common.Interfaces;

namespace TaskManager.Application.Attachments.Commands.DeleteAttachment
{
    /// <summary>
    /// Запрос на удаление вложения
    /// </summary>
    public class DeleteAttachmentCommand : IRequest
    {
        public DeleteAttachmentCommand(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Идентификатор вложения
        /// </summary>
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Обработчик запроса на удаление вложения
    /// </summary>
    public class DeleteAttachmentCommandHandler : IRequestHandler<DeleteAttachmentCommand>
    {
        private readonly IAttachmentService _attachmentService;

        public DeleteAttachmentCommandHandler(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        public async Task<Unit> Handle(DeleteAttachmentCommand request, CancellationToken cancellationToken)
        {
            await _attachmentService.Delete(request.Id, cancellationToken);

            return Unit.Value;
        }
    }
}
