using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Common.Interfaces;

namespace TaskManager.Application.Attachments.Commands.AddAttachment
{
    public class AddAttachmentCommand : IRequest<AddAttachmentResponse>
    {
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        [Required]
        public Guid TaskId { get; set; }

        /// <summary>
        /// Вложенный файл
        /// </summary>
        [Required]
        public IFormFile File { get; set; }
    }

    public class AddAttachmentCommandHandler : IRequestHandler<AddAttachmentCommand, AddAttachmentResponse>
    {
        private readonly IAttachmentService _attachmentService;

        public AddAttachmentCommandHandler(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        public async Task<AddAttachmentResponse> Handle(AddAttachmentCommand request, CancellationToken cancellationToken)
        {
            return new AddAttachmentResponse(await _attachmentService.Add(request.File, request.TaskId, cancellationToken));
        }
    }
}
