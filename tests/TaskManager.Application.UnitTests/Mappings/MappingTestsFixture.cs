using AutoMapper;

namespace TaskManager.Application.UnitTests.Mappings
{
    public class MappingTestsFixture 
    {
        public MappingTestsFixture()
        {
            ConfigurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(new[] {
                    "TaskManager.Application"
                });
            });

            Mapper = ConfigurationProvider.CreateMapper();
        }

        public IConfigurationProvider ConfigurationProvider { get; }

        public IMapper Mapper { get; }
    }
}
