using Condominio.Domain.Shared;
using Condominio.Domain.Usuarios;
using Condominio.Domain.Viviendas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Condominio.Application.Usuarios
{
    public class UsuarioDetailsViewModel
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public RolUsuario Rol { get; set; }
        public EstadoGeneral Estado { get; set; }

        public DateTime? FechaIngreso { get; set; }

        public EstadoGeneral? EstadoResidente { get; set; }

        public string Vivienda { get; set; }
    }
}