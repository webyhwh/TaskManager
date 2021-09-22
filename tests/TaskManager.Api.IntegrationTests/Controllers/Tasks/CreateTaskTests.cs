using System.IO;
using System.Net.Http;
using System.Text;
using TaskManager.Api.IntegrationTests.Common;
using TaskManager.Domain.Entities;
using Xunit;

namespace TaskManager.Api.IntegrationTests.Controllers.Tasks
{
    public class CreateTaskTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public CreateTaskTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async System.Threading.Tasks.Task GivenCreateTaskCommand_ReturnsSuccessStatusCode()
        {
            var client = _factory.GetAnonymousClient();

            using (var formContent = new MultipartFormDataContent())
            {
                formContent.Headers.ContentType.MediaType = "multipart/form-data";
                formContent.Add(new StringContent("Вынести мусор"), "Name");
                formContent.Add(new StringContent(nameof(Status.Active)), "Status");
                formContent.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes("Test file 1"))), "Files", "test1.txt");
                formContent.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes("Test file 2"))), "Files", "test2.txt");

                var response = await client.PostAsync("/api/tasks", formContent);

                var responseContent = response.Content.ReadAsStringAsync().Result;

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
