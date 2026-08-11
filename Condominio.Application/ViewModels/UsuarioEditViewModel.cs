using Condominio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Condominio.Application.ViewModels.Usuario
{
    public class UsuarioEditViewModel
    {
        public int IdUsuario { get; set; }

        public string Nombre { get; set; }

        public string Correo { get; set; }

        public string Telefono { get; set; }

        public RolUsuario Rol { get; set; }

        public EstadoGeneral Estado { get; set; }

        [Display(Name = "Fecha de ingreso")]
        public DateTime? FechaIngreso { get; set; }

        [Display(Name = "Estado del residente")]
        public EstadoGeneral? EstadoResidente { get; set; }

        [Display(Name = "Vivienda")]
        public int? IdVivienda { get; set; }

        public IEnumerable<SelectListItem> Viviendas { get; set; }
    }
}
