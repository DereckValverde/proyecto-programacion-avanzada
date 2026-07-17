using AutoMapper;

namespace proyecto_programacion_avanzada.Mappings
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