using System.Threading.Tasks;
using TaskManager.Api.IntegrationTests.Common;
using Xunit;

namespace TaskManager.Api.IntegrationTests.Controllers.Attachments
{
    public class DeleteAttachmentTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public DeleteAttachmentTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenDeleteAttachmentCommand_ReturnsSuccessStatusCode()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.DeleteAsync("/api/attachments/91AE4A39-D64E-4E11-21F2-08D97D15F28E");
            response.EnsureSuccessStatusCode();
        }
    }
}
