using AutoMapper;
using proyecto_programacion_avanzada.DTOs;
using proyecto_programacion_avanzada.Entities;
using proyecto_programacion_avanzada.ViewModels.Residente;
using proyecto_programacion_avanzada.ViewModels.Usuario;
using proyecto_programacion_avanzada.ViewModels.Vivienda;

namespace proyecto_programacion_avanzada.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario, UsuarioDto>()
                .ReverseMap();

            CreateMap<UsuarioDto, UsuarioCreateViewModel>()
                .ForMember(dest => dest.FechaIngreso, opt => opt.Ignore())
                .ForMember(dest => dest.IdVivienda, opt => opt.Ignore())
                .ForMember(dest => dest.Viviendas, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UsuarioDto, UsuarioEditViewModel>()
                .ForMember(dest => dest.FechaIngreso, opt => opt.Ignore())
                .ForMember(dest => dest.EstadoResidente, opt => opt.Ignore())
                .ForMember(dest => dest.IdVivienda, opt => opt.Ignore())
                .ForMember(dest => dest.Viviendas, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UsuarioDto, UsuarioDetailsViewModel>()
                .ForMember(dest => dest.FechaIngreso, opt => opt.Ignore())
                .ForMember(dest => dest.EstadoResidente, opt => opt.Ignore())
                .ForMember(dest => dest.Vivienda, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UsuarioDto, UsuarioListViewModel>()
                .ReverseMap();


            CreateMap<Vivienda, ViviendaDto>()
                .ReverseMap();

            CreateMap<ViviendaDto, ViviendaCreateViewModel>()
                .ReverseMap();

            CreateMap<ViviendaDto, ViviendaEditViewModel>()
                .ReverseMap();

            CreateMap<ViviendaDto, ViviendaListViewModel>()
                .ReverseMap();

            CreateMap<ViviendaDto, ViviendaDetailsViewModel>()
                .ReverseMap();


            CreateMap<Residente, ResidenteDto>()
                .ForMember(dest => dest.NombreUsuario,
                    opt => opt.MapFrom(src => src.Usuario.Nombre))
                .ForMember(dest => dest.NombreVivienda,
                    opt => opt.MapFrom(src =>
                        "Bloque " + src.Vivienda.Bloque +
                        " - Vivienda " + src.Vivienda.Numero));


            CreateMap<ResidenteDto, Residente>()
                .ForMember(dest => dest.Usuario, opt => opt.Ignore())
                .ForMember(dest => dest.Vivienda, opt => opt.Ignore())
                .ForMember(dest => dest.Incidencias, opt => opt.Ignore());


            CreateMap<ResidenteDto, ResidenteCreateViewModel>()
                .ForMember(dest => dest.Usuarios, opt => opt.Ignore())
                .ForMember(dest => dest.Viviendas, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResidenteDto, ResidenteEditViewModel>()
                .ForMember(dest => dest.Usuarios, opt => opt.Ignore())
                .ForMember(dest => dest.Viviendas, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResidenteDto, ResidenteListViewModel>()
                .ForMember(dest => dest.Vivienda,
                    opt => opt.MapFrom(src => src.NombreVivienda));

            CreateMap<ResidenteDto, ResidenteDetailsViewModel>()
                .ReverseMap();


            CreateMap<Pago, PagoDto>()
                .ReverseMap();

            CreateMap<AreaComun, AreaComunDto>()
                .ReverseMap();

            CreateMap<Reserva, ReservaDto>()
                .ReverseMap();

            CreateMap<Visitante, VisitanteDto>()
                .ReverseMap();

            CreateMap<Incidencia, IncidenciaDto>()
                .ReverseMap();
        }
    }
}