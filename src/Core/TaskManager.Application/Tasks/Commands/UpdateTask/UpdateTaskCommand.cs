using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Common.Interfaces;

namespace TaskManager.Application.Tasks.Commands.UpdateTask
{
    /// <summary>
    /// Запрос на обновление задачи
    /// </summary>
    public class UpdateTaskCommand : IRequest
    {
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Название задачи
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Статус задачи
        /// </summary>
        public Domain.Entities.Status? Status { get; set; }
    }

    /// <summary>
    /// Обработчик запроса на обновление задачи
    /// </summary>
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
    {
        private readonly ITaskManagerDbContext _context;

        public UpdateTaskCommandHandler(ITaskManagerDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks
               .SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (task == null)
            {
                throw new Exception($"Задача с Id = {request.Id} не найдена");
            }

            if (!String.IsNullOrEmpty(request.Name))
            {
                task.Name = request.Name;
            }

            if (request.Status.HasValue)
            {
                task.Status = request.Status.Value;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
