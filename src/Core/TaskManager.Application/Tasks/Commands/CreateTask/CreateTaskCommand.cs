using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Entities;
using Status = TaskManager.Domain.Entities.Status;

namespace TaskManager.Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommand : IRequest<CreateTaskResponse>
    {
        /// <summary>
        /// Название задачи
        /// </summary>
        /// <example>Погулять с собакой</example>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Статус задачи
        /// </summary>
        [Required]
        public Status Status { get; set; }

        /// <summary>
        /// Вложенные файлы
        /// </summary>
        public IEnumerable<IFormFile> Files { get; set; }
    }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, CreateTaskResponse>
    {
        private readonly ITaskManagerDbContext _context;

        private readonly IFileStorage _fileStorage;

        public CreateTaskCommandHandler(ITaskManagerDbContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        public async Task<CreateTaskResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new Domain.Entities.Task
            {
                Name = request.Name,
                Status = request.Status,
            };

            _context.Tasks.Add(task);

            await _context.SaveChangesAsync(cancellationToken);

            if (request.Files?.Any() == true)
            {
                var attachments = new List<Attachment>();

                foreach (var file in request.Files)
                {
                    var filePath = await _fileStorage.Store(file, task.Id);

                    attachments.Add(new Attachment
                    {
                        Name = file.FileName,
                        Url = filePath
                    });
                }

                task.Attachments = attachments;

                _context.Tasks.Update(task);

                await _context.SaveChangesAsync(cancellationToken);
            }

            return new CreateTaskResponse(task.Id);
        }
    }
}
