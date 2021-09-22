using System.Threading.Tasks;
using TaskManager.Api.IntegrationTests.Common;
using Xunit;

namespace TaskManager.Api.IntegrationTests.Controllers.Tasks
{
    public class DeleteTaskTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public DeleteTaskTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenDeleteTaskCommand_ReturnsSuccessStatusCode()
        {
            var client = _factory.GetAnonymousClient();
            var response = await client.DeleteAsync("/api/tasks/098a277f-88ab-46f3-ae28-e97156a1f654");
            response.EnsureSuccessStatusCode();
        }
    }
}
