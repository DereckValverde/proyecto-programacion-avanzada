using AutoMapper;
using Condominio.Application.AreasComunes;
using Condominio.Application.Incidencias;
using Condominio.Application.Noticias;
using Condominio.Application.Pagos;
using Condominio.Application.Reservas;
using Condominio.Application.Residentes;
using Condominio.Application.Usuarios;
using Condominio.Application.Visitantes;
using Condominio.Application.Viviendas;
using Condominio.Domain.AreasComunes;
using Condominio.Domain.Incidencias;
using Condominio.Domain.Noticias;
using Condominio.Domain.Pagos;
using Condominio.Domain.Reservas;
using Condominio.Domain.Residentes;
using Condominio.Domain.Usuarios;
using Condominio.Domain.Visitantes;
using Condominio.Domain.Viviendas;

namespace Condominio.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario, UsuarioDto>()
                .ForMember(dest => dest.IdUsuario, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Contrasena, opt => opt.MapFrom(src => src.PasswordHash));

            CreateMap<UsuarioDto, Usuario>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdUsuario))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Correo))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Contrasena))
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEndDateUtc, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
                .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore())
                .ForMember(dest => dest.Roles, opt => opt.Ignore())
                .ForMember(dest => dest.Claims, opt => opt.Ignore())
                .ForMember(dest => dest.Logins, opt => opt.Ignore());

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


            CreateMap<Pago, PagoDto>()
                .ForMember(dest => dest.NombreVivienda,
                    opt => opt.MapFrom(src =>
                        "Bloque " + src.Vivienda.Bloque +
                        " - Vivienda " + src.Vivienda.Numero));

            CreateMap<PagoDto, Pago>()
                .ForMember(dest => dest.Vivienda, opt => opt.Ignore());

            CreateMap<PagoDto, PagoCreateViewModel>()
                .ForMember(dest => dest.Viviendas, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<PagoDto, PagoEditViewModel>()
                .ForMember(dest => dest.Viviendas, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<PagoDto, PagoListViewModel>()
                .ReverseMap();

            CreateMap<PagoDto, PagoDetailsViewModel>()
                .ReverseMap();

            CreateMap<AreaComun, AreaComunDto>()
                .ReverseMap();

            CreateMap<Reserva, ReservaDto>()
                .ForMember(dest => dest.NombreVivienda,
                opt => opt.MapFrom(src =>
                "Bloque " + src.Vivienda.Bloque + " - Vivienda " + src.Vivienda.Numero))
                .ForMember(dest => dest.NombreArea,
                opt => opt.MapFrom(src => src.AreaComun.Nombre));

            CreateMap<ReservaDto, Reserva>()
                .ForMember(dest => dest.Vivienda, opt => opt.Ignore())
                .ForMember(dest => dest.AreaComun, opt => opt.Ignore());

            CreateMap<ReservaDto, ReservaCreateViewModel>()
                .ForMember(dest => dest.Viviendas, opt => opt.Ignore())
                .ForMember(dest => dest.AreasComunes, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ReservaDto, ReservaEditViewModel>()
                .ForMember(dest => dest.Viviendas, opt => opt.Ignore())
                .ForMember(dest => dest.AreasComunes, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ReservaDto, ReservaListViewModel>()
                .ReverseMap();

            CreateMap<ReservaDto, ReservaDetailsViewModel>()
                .ReverseMap();

            CreateMap<Visitante, VisitanteDto>()
                .ForMember(dest => dest.NombreVivienda,
                    opt => opt.MapFrom(src =>
                        "Bloque " + src.Vivienda.Bloque +
                        " - Vivienda " + src.Vivienda.Numero));

            CreateMap<VisitanteDto, Visitante>()
                .ForMember(dest => dest.Vivienda, opt => opt.Ignore());

            CreateMap<VisitanteDto, VisitanteIngresoViewModel>()
                .ForMember(dest => dest.Viviendas, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<VisitanteDto, VisitanteEditViewModel>()
                .ForMember(dest => dest.Viviendas, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<VisitanteDto, VisitanteListViewModel>()
                .ReverseMap();

            CreateMap<VisitanteDto, VisitanteDetailsViewModel>()
                .ReverseMap();

            CreateMap<Incidencia, IncidenciaDto>()
                .ForMember(dest => dest.NombreResidente,
                    opt => opt.MapFrom(src => src.Residente.Nombre))
                .ForMember(dest => dest.NombreVivienda,
                    opt => opt.MapFrom(src =>
                        "Bloque " + src.Residente.Vivienda.Bloque +
                        " - Vivienda " + src.Residente.Vivienda.Numero));

            CreateMap<IncidenciaDto, Incidencia>()
                .ForMember(dest => dest.Residente, opt => opt.Ignore());

            CreateMap<IncidenciaDto, IncidenciaCreateViewModel>()
                .ReverseMap();

            CreateMap<IncidenciaDto, IncidenciaEditViewModel>()
                .ReverseMap();

            CreateMap<IncidenciaDto, IncidenciaListViewModel>()
                .ReverseMap();

            CreateMap<IncidenciaDto, IncidenciaDetailsViewModel>()
                .ReverseMap();

            CreateMap<Noticia, NoticiaDto>()
                .ReverseMap();

            CreateMap<NoticiaDto, NoticiaCreateViewModel>()
                .ReverseMap();

            CreateMap<NoticiaDto, NoticiaEditViewModel>()
                .ReverseMap();

            CreateMap<NoticiaDto, NoticiaListViewModel>()
                .ReverseMap();

            CreateMap<NoticiaDto, NoticiaDetailsViewModel>()
                .ReverseMap();
        }
    }
}