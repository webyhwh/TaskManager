using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Tasks.Queries.GetTasks
{
    /// <summary>
    /// Запрос на получение задач
    /// </summary>
    public class GetTasksQuery : IRequest<IEnumerable<TaskDto>>
    {
        /// <summary>
        /// Номер страницы
        /// </summary>
        /// <example>1</example>
        public int PageNumber { get; set; }

        /// <summary>
        /// Кол-во записей на странице
        /// </summary>
        /// <example>10</example>
        [Required]
        public int PageSize { get; set; }

        /// <summary>
        /// Статус задачи
        /// </summary>
        /// <example>new</example>
        [Required]
        public Status? Status { get; set; }
    }

    /// <summary>
    /// Обработчик запроса на получение задач
    /// </summary>
    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, IEnumerable<TaskDto>>
    {
        private readonly ITaskManagerDbContext _context;
        private readonly IMapper _mapper;

        public GetTasksQueryHandler(ITaskManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            return await _context.Tasks
                .AsNoTracking()
                .Where(t => !request.Status.HasValue || t.Status == request.Status.Value)
                .OrderByDescending(t => t.CreatedDate)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<TaskDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
