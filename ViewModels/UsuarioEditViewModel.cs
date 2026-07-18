using proyecto_programacion_avanzada.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace proyecto_programacion_avanzada.ViewModels
{
    public class UsuarioEditViewModel
    {
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Correo { get; set; }

        [Phone]
        [StringLength(20)]
        public string Telefono { get; set; }

        [Required]
        public RolUsuario Rol { get; set; }

        [Required]
        public EstadoGeneral Estado { get; set; }
    }
}