using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskManager.Api.IntegrationTests.Common;
using TaskManager.Application.Tasks.Commands.UpdateTask;
using Xunit;

namespace TaskManager.Api.IntegrationTests.Controllers.Tasks
{
    public class UpdateTaskTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public UpdateTaskTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenUpdateTaskCommand_ReturnsSuccessStatusCode()
        {
            var client = _factory.GetAnonymousClient();

            var command = new UpdateTaskCommand
            {
                Id = new Guid("22eafec1-64af-41c5-b462-02e4a00490b5"),
                Name = "Updated",
                Status = Domain.Entities.Status.Resolved
            };

            var response = await client.PatchAsync("/api/tasks/", new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
        }
    }
}
