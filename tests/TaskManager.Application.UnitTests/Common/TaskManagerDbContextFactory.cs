using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using TaskManager.Domain.Entities;
using TaskManager.Persistence;

namespace TaskManager.Application.UnitTests.Common
{
    public static class TaskManagerDbContextFactory
    {
        public static TaskManagerDbContext Create()
        {
            var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new TaskManagerDbContext(options);

            context.Database.EnsureCreated();

            context.Tasks.AddRange(new[] {
                new Domain.Entities.Task {
                    Id = new Guid("5363a4d0-3898-4672-858a-fc80eb083f2f"),
                    Name = "1",
                    Status = Status.Active,
                    Attachments = new List<Attachment>
                    {
                        new Attachment
                        {
                            Id = new Guid("f41303f8-5828-4a65-8609-d42a19bdc5aa"),
                            Name = "test.txt",
                            Url = @"C:\testPath\test.txt"
                        }
                    }
                }
            });

            context.SaveChanges();

            return context;
        }

        public static void Destroy(TaskManagerDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
