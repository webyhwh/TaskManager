using Shouldly;
using System.Net;
using System.Threading.Tasks;
using TaskManager.Api.IntegrationTests.Common;
using Xunit;

namespace TaskManager.Api.IntegrationTests.Controllers.Tasks
{
    public class GetTasksTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public GetTasksTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenValidGetTasksQuery_ReturnsSuccessStatusCode()
        {
            var client = _factory.GetAnonymousClient();
            var response = await client.GetAsync("/api/tasks?status=new&pageNumber=1&pageSize=10");

            response.EnsureSuccessStatusCode();
        }


        [Fact]
        public async Task GivenInvalidGetTasksQuery_ReturnsBadRequest()
        {
            var client = _factory.GetAnonymousClient();
            var response = await client.GetAsync("/api/tasks?status=new");
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.BadRequest);
        }
    }
}
