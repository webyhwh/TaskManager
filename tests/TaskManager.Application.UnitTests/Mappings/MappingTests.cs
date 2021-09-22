using AutoMapper;
using Shouldly;
using TaskManager.Application.Tasks.Queries.GetTasks;
using Xunit;

namespace TaskManager.Application.UnitTests.Mappings
{
    public class MappingTests : IClassFixture<MappingTestsFixture>
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests(MappingTestsFixture fixture)
        {
            _configuration = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void ShouldMapTaskToTaskDto()
        {
            var entity = new Domain.Entities.Task();

            var result = _mapper.Map<TaskDto>(entity);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<TaskDto>();
        }

        [Fact]
        public void ShouldMapAttachmentToAttachmentDto()
        {
            var entity = new Domain.Entities.Attachment();

            var result = _mapper.Map<AttachmentDto>(entity);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<AttachmentDto>();
        }
    }
}
