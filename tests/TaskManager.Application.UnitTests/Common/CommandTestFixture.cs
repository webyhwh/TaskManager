using System;
using TaskManager.Persistence;
using Xunit;

namespace TaskManager.Application.UnitTests.Common
{
    public class CommandTestFixture : IDisposable
    {
        public TaskManagerDbContext Context { get; }

        public CommandTestFixture()
        {
            Context = TaskManagerDbContextFactory.Create();
        }

        public void Dispose()
        {
            TaskManagerDbContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("CommandCollection")]
    public class CommandCollection : ICollectionFixture<CommandTestFixture> { }
}
