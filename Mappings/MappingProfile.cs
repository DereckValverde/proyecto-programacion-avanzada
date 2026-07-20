using AutoMapper;
using proyecto_programacion_avanzada.DTOs;
using proyecto_programacion_avanzada.Entities;
using proyecto_programacion_avanzada.ViewModels;
using proyecto_programacion_avanzada.ViewModels.Residente;
using proyecto_programacion_avanzada.ViewModels.Usuario;
using proyecto_programacion_avanzada.ViewModels.Vivienda;

namespace proyecto_programacion_avanzada.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario, UsuarioDto>().ReverseMap();

            CreateMap<UsuarioDto, UsuarioCreateViewModel>().ReverseMap();
            CreateMap<UsuarioDto, UsuarioEditViewModel>().ReverseMap();
            CreateMap<UsuarioDto, UsuarioListViewModel>().ReverseMap();
            CreateMap<UsuarioDto, UsuarioDetailsViewModel>().ReverseMap();

            CreateMap<Vivienda, ViviendaDto>().ReverseMap();
            CreateMap<ViviendaDto, ViviendaCreateViewModel>().ReverseMap();
            CreateMap<ViviendaDto, ViviendaEditViewModel>().ReverseMap();
            CreateMap<ViviendaDto, ViviendaListViewModel>().ReverseMap();
            CreateMap<ViviendaDto, ViviendaDetailsViewModel>().ReverseMap();

            CreateMap<Residente, ResidenteDto>().ReverseMap();

            CreateMap<ResidenteDto, ResidenteCreateViewModel>()
                .ForMember(dest => dest.Usuarios, opt => opt.Ignore())
                .ForMember(dest => dest.Viviendas, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResidenteDto, ResidenteEditViewModel>()
                .ForMember(dest => dest.Usuarios, opt => opt.Ignore())
                .ForMember(dest => dest.Viviendas, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResidenteDto, ResidenteListViewModel>()
                .ForMember(dest => dest.NombreUsuario, opt => opt.Ignore())
                .ForMember(dest => dest.NombreVivienda, opt => opt.Ignore());

            CreateMap<ResidenteDto, ResidenteDetailsViewModel>()
                .ForMember(dest => dest.NombreUsuario, opt => opt.Ignore())
                .ForMember(dest => dest.NombreVivienda, opt => opt.Ignore());

            CreateMap<Pago, PagoDto>().ReverseMap();
            CreateMap<AreaComun, AreaComunDto>().ReverseMap();
            CreateMap<Reserva, ReservaDto>().ReverseMap();
            CreateMap<Visitante, VisitanteDto>().ReverseMap();
            CreateMap<Incidencia, IncidenciaDto>().ReverseMap();
        }
    }
}