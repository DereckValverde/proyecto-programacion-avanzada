using AutoMapper;
using proyecto_programacion_avanzada.DTOs;
using proyecto_programacion_avanzada.Entities;
using proyecto_programacion_avanzada.ViewModels;

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
            CreateMap<Residente, ResidenteDto>().ReverseMap();
            CreateMap<Vivienda, ViviendaDto>().ReverseMap();
            CreateMap<Pago, PagoDto>().ReverseMap();
            CreateMap<AreaComun, AreaComunDto>().ReverseMap();
            CreateMap<Reserva, ReservaDto>().ReverseMap();
            CreateMap<Visitante, VisitanteDto>().ReverseMap();
            CreateMap<Incidencia, IncidenciaDto>().ReverseMap();
        }
    }
}