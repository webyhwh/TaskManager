using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Persistence;

namespace TaskManager.Api.IntegrationTests.Common
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .ConfigureServices(services =>
                {
                    var serviceProvider = new ServiceCollection()
                        .AddEntityFrameworkInMemoryDatabase()
                        .BuildServiceProvider();

                    services.AddDbContext<TaskManagerDbContext>(options =>
                    {
                        options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                        options.UseInternalServiceProvider(serviceProvider);
                    });

                    services.AddScoped<ITaskManagerDbContext>(provider => provider.GetService<TaskManagerDbContext>());

                    var sp = services.BuildServiceProvider();

                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<TaskManagerDbContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    context.Database.EnsureCreated();

                    try
                    {
                        context.Tasks.Add(new Domain.Entities.Task
                        {
                            Id = new Guid("22eafec1-64af-41c5-b462-02e4a00490b5"),
                            Name = "Test Task",
                            Status = Domain.Entities.Status.Active
                        });

                        context.Tasks.Add(new Domain.Entities.Task
                        {
                            Id = new Guid("098a277f-88ab-46f3-ae28-e97156a1f654"),
                            Name = "Test Task 2",
                            Status = Domain.Entities.Status.Active
                        });

                        context.Attachments.Add(new Domain.Entities.Attachment
                        {
                            Id = new Guid("91AE4A39-D64E-4E11-21F2-08D97D15F28E"),
                            Name = "Test Attachment",
                            TaskId = new Guid("22eafec1-64af-41c5-b462-02e4a00490b5"),
                            Url = "C:\\TaskManager\\Attachments\\22eafec1-64af-41c5-b462-02e4a00490b5\\dummy.txt"
                        });

                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            $"database with test messages. Error: {ex.Message}");
                    }
                });
        }

        public HttpClient GetAnonymousClient()
        {
            return CreateClient();
        }
    }
}