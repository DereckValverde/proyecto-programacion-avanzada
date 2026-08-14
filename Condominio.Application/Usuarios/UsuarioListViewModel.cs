using Condominio.Domain.Shared;
using Condominio.Domain.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Condominio.Application.Usuarios
{
    public class UsuarioListViewModel
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public RolUsuario Rol { get; set; }
        public EstadoGeneral Estado { get; set; }

        public string RolNombre => Rol.ToString();

        public string EstadoNombre => Estado.ToString();
    }
}