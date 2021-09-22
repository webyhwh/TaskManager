using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Application.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly ITaskManagerDbContext _context;
        private readonly IFileStorage _fileStorage;

        public AttachmentService(IFileStorage fileStorage, ITaskManagerDbContext context)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        public async Task<Guid> Add(IFormFile file, Guid taskId, CancellationToken cancellationToken)
        {
            var filePath = await _fileStorage.Store(file, taskId);
            var attachment = new Attachment()
            {
                Name = file.FileName,
                Url = filePath,
                TaskId = taskId
            };

            await _context.Attachments.AddAsync(attachment);
            await _context.SaveChangesAsync(cancellationToken);

            return attachment.Id;
        }

        public async Task Delete(Guid attachmentId, CancellationToken cancellationToken)
        {
            var attachment = await _context.Attachments.FindAsync(attachmentId);

            if (attachment == null)
            {
                throw new Exception($"Вложение с Id = {attachmentId} не найдено");
            }

            _fileStorage.Delete(attachment.Url);

            _context.Attachments.Remove(attachment);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
