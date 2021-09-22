using AutoMapper;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Tasks.Queries.GetTasks;
using TaskManager.Application.UnitTests.Common;
using TaskManager.Persistence;
using Xunit;

namespace TaskManager.Application.UnitTests.Tasks.Queries.GetTasks
{
    [Collection("QueryCollection")]
    public class GetTasksQueryTests
    {
        private readonly TaskManagerDbContext _context;
        private readonly IMapper _mapper;

        public GetTasksQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldReturnTasks()
        {
            var sut = new GetTasksQueryHandler(_context, _mapper);

            var result = await sut.Handle(new GetTasksQuery { Status = Domain.Entities.Status.Active, PageNumber = 1, PageSize = 10 }, CancellationToken.None);

            result.ShouldBeOfType<List<TaskDto>>();
            result.ShouldNotBeNull();
        }
    }
}
