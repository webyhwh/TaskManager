using Microsoft.AspNetCore.Http;
using Moq;
using Shouldly;
using System;
using System.IO;
using System.Text;
using System.Threading;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Application.UnitTests.Common;
using TaskManager.Persistence;
using Xunit;

namespace TaskManager.Application.UnitTests.Services
{
    public class AttachmentServiceTests : IClassFixture<CommandTestFixture>
    {
        private readonly TaskManagerDbContext _context;

        public AttachmentServiceTests(CommandTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async void Handle_GivenValidRequest_ShouldCreateAttachment()
        {
            // Arrange
            var fileStorageMock = new Mock<IFileStorage>();
            fileStorageMock.Setup(x => x.Store(It.IsAny<IFormFile>(), It.IsAny<Guid>())).ReturnsAsync(@"C:\testPath\test.txt");

            var sut = new AttachmentService(fileStorageMock.Object, _context);
            var fileMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes("Test file text"));
            var formFile = new FormFile(fileMemoryStream, 0, fileMemoryStream.Length, "File", "test.txt");

            // Act
            var attachmentGuid = await sut.Add(formFile, new Guid("9F48EF05-9FE1-4C74-F327-08D97D41D1F7"), CancellationToken.None);

            // Assert
            var attachment = await _context.Attachments.FindAsync(attachmentGuid);
            attachment.ShouldNotBeNull();
        }

        [Fact]
        public async void Handle_GivenValidRequest_ShouldDeleteAttachment()
        {
            // Arrange
            var fileStorageMock = new Mock<IFileStorage>();
            fileStorageMock.Setup(x => x.Delete(@"C:\testPath\test.txt"));
            var existingAttachmentId = new Guid("f41303f8-5828-4a65-8609-d42a19bdc5aa");

            var sut = new AttachmentService(fileStorageMock.Object, _context);

            // Act
            await sut.Delete(existingAttachmentId, CancellationToken.None);

            // Assert
            var attachment = await _context.Attachments.FindAsync(existingAttachmentId);
            attachment.ShouldBeNull();
        }
    }
}
