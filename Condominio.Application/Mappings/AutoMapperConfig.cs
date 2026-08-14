using AutoMapper;

namespace Condominio.Application.Mappings
{
    public static class AutoMapperConfig
    {
        public static IMapper Mapper { get; private set; }

        public static void RegisterMappings()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            config.AssertConfigurationIsValid();

            Mapper = config.CreateMapper();
        }
    }
}