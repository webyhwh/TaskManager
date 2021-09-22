using AutoMapper;
using System;
using TaskManager.Persistence;
using Xunit;

namespace TaskManager.Application.UnitTests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public TaskManagerDbContext Context { get; }
        public IMapper Mapper { get; }

        public QueryTestFixture()
        {
            Context = TaskManagerDbContextFactory.Create();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(new[] {
                    "TaskManager.Application"
                });
            });

            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            TaskManagerDbContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
