using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Common.Interfaces;

namespace TaskManager.Application.Tasks.Commands.DeleteTask
{
    /// <summary>
    /// Запрос на удаление задачи
    /// </summary>
    public class DeleteTaskCommand : IRequest
    {
        public DeleteTaskCommand(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Обработчик запроса на удаление задачи
    /// </summary>
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly ITaskManagerDbContext _context;

        public DeleteTaskCommandHandler(ITaskManagerDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks.FindAsync(request.Id);

            if (task == null)
            {
                throw new Exception($"Задача с Id = {request.Id} не найдена");
            }

            _context.Tasks.Remove(task);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
