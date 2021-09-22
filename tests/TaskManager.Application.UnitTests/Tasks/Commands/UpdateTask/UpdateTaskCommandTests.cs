using Shouldly;
using System;
using System.Threading;
using TaskManager.Application.Tasks.Commands.UpdateTask;
using TaskManager.Application.UnitTests.Common;
using Xunit;

namespace TaskManager.Application.UnitTests.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommandTests : CommandTestFixture
    {
        [Fact]
        public async void Handle_GivenExistingTaskId_ShouldUpdateTask()
        {
            // Arrange
            var sut = new UpdateTaskCommandHandler(Context);
            var existingTaskId = new Guid("5363a4d0-3898-4672-858a-fc80eb083f2f");

            // Act
            await sut.Handle(new UpdateTaskCommand { Id = existingTaskId, Status = Domain.Entities.Status.Closed }, CancellationToken.None);

            // Assert
            var task = await Context.Tasks.FindAsync(existingTaskId);
            task.Status.ShouldBeEquivalentTo(Domain.Entities.Status.Closed);
        }
    }
}
