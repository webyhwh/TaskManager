using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TaskManager.Persistence.IntegrationTests
{
    public class TaskManagerDbContextTests : IDisposable
    {
        private readonly TaskManagerDbContext _sut;

        public TaskManagerDbContextTests()
        {
            var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _sut = new TaskManagerDbContext(options);

            _sut.Tasks.Add(new Domain.Entities.Task
            {
                Id = new Guid("6a09de70-5e0e-49d1-8681-d5a34c42012b"),
                Name = "Задача для тестирования",
                Status = Domain.Entities.Status.New
            });

            _sut.SaveChanges();
        }

        [Fact]
        public async Task SaveChangesAsync_GivenNewTask_ShouldSetCreatedDate()
        {
            var task = new Domain.Entities.Task
            {
                Id = new Guid("63339b9b-722a-45b6-bb73-c38fb922f49a"),
                Name = "Задача для тестирования #2"
            };

            await _sut.Tasks.AddAsync(task);
            await _sut.SaveChangesAsync();

            task.CreatedDate.ShouldNotBe(DateTime.MinValue);
        }

        [Fact]
        public async Task SaveChangesAsync_GivenExistingTask_ShouldSetLastModifiedDate()
        {
            var task = await _sut.Tasks.FindAsync(new Guid("6a09de70-5e0e-49d1-8681-d5a34c42012b"));

            task.Status = Domain.Entities.Status.Active;

            await _sut.SaveChangesAsync();

            task.LastModifiedDate.ShouldNotBeNull();
            task.LastModifiedDate.ShouldNotBe(DateTime.MinValue);
        }

        public void Dispose()
        {
            _sut?.Dispose();
        }
    }
}
