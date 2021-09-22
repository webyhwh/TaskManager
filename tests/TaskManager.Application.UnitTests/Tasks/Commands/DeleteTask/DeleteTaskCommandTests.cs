using Shouldly;
using System;
using System.Threading;
using TaskManager.Application.Tasks.Commands.DeleteTask;
using TaskManager.Application.UnitTests.Common;
using Xunit;

namespace TaskManager.Application.UnitTests.Tasks.Commands.DeleteTask
{
    public class DeleteTaskCommandTests : CommandTestFixture
    {
        [Fact]
        public async void Handle_GivenExistingTaskId_ShouldDeleteTask()
        {
            // Arrange
            var sut = new DeleteTaskCommandHandler(Context);
            var existingTaskId = new Guid("5363a4d0-3898-4672-858a-fc80eb083f2f");

            // Act
            await sut.Handle(new DeleteTaskCommand(existingTaskId), CancellationToken.None);

            // Assert
            var task = await Context.Tasks.FindAsync(existingTaskId);
            task.ShouldBeNull();
        }
    }
}
