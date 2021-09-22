using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Common.Interfaces;

namespace TaskManager.Application.System.Commands.SeedSampleData
{
    public class SeedSampleDataCommand : IRequest
    {

    }

    public class SeedSampleDataCommandHandler : IRequestHandler<SeedSampleDataCommand>
    {
        private readonly ITaskManagerDbContext _context;

        public SeedSampleDataCommandHandler(ITaskManagerDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(SeedSampleDataCommand request, CancellationToken cancellationToken)
        {
            if (_context.Tasks.Any())
            {
                return Unit.Value;
            }

            var tasks = new List<Domain.Entities.Task>();
            var random = new Random();
            for (var i = 1; i < 2000; i++)
            {
                var values = Enum.GetValues(typeof(Domain.Entities.Status));
                var randomStatus = (Domain.Entities.Status)values.GetValue(random.Next(values.Length));

                tasks.Add(new Domain.Entities.Task()
                {
                    Name = $"Тестовая задача № {i}",
                    Status = randomStatus
                });
            }

            await _context.Tasks.AddRangeAsync(tasks);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
