using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Application.Attachments.Commands.AddAttachment
{
    /// <summary>
    /// Ответ запроса на добавление вложения
    /// </summary>
    public class AddAttachmentResponse
    {
        public AddAttachmentResponse(Guid attachmentId)
        {
            AttachmentId = attachmentId;
        }

        /// <summary>
        /// Идентификатор вложения
        /// </summary>
        [Required]
        public Guid AttachmentId { get; set; }
    }
}
