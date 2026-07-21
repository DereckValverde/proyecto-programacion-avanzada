using proyecto_programacion_avanzada.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyecto_programacion_avanzada.ViewModels.Usuario
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