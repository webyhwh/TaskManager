using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Api.IntegrationTests.Common;
using Xunit;

namespace TaskManager.Api.IntegrationTests.Controllers.Attachments
{
    public class AddAttachmentTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public AddAttachmentTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenAddAttachmentCommand_ReturnsSuccessStatusCode()
        {
            var client = _factory.GetAnonymousClient();

            using (var formContent = new MultipartFormDataContent())
            {
                formContent.Headers.ContentType.MediaType = "multipart/form-data";
                formContent.Add(new StringContent("22eafec1-64af-41c5-b462-02e4a00490b5"), "TaskId");
                formContent.Add(new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes("Test file"))), "File", "dummy.txt");

                var response = await client.PostAsync("/api/attachments", formContent);

                var responseContent = response.Content.ReadAsStringAsync().Result;

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
