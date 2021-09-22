using Microsoft.AspNetCore.Http;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Tasks.Commands.CreateTask;
using TaskManager.Application.UnitTests.Common;
using Xunit;

namespace TaskManager.Application.UnitTests.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandTests : CommandTestFixture
    {
        [Fact]
        public async void Handle_GivenValidRequestWithFiles_ShouldCreateTask()
        {
            // Arrange
            var fileStorageMock = new Mock<IFileStorage>();
            fileStorageMock.Setup(x => x.Store(It.IsAny<IFormFile>(), It.IsAny<Guid>())).ReturnsAsync(@"C:\testPath\test.txt");

            var sut = new CreateTaskCommandHandler(Context, fileStorageMock.Object);

            var fileMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes("Test file text"));
            var command = new CreateTaskCommand
            {
                Name = "Тестовая задача №999999",
                Status = Domain.Entities.Status.New,
                Files = new List<IFormFile>
                {
                    new FormFile(fileMemoryStream, 0, fileMemoryStream.Length, "File", "test.txt")
                }
            };

            // Act
            var createTaskResponse = await sut.Handle(command, CancellationToken.None);

            // Assert
            var task = await Context.Tasks.FindAsync(createTaskResponse.TaskId);
            task.ShouldNotBeNull();
        }
    }
}
