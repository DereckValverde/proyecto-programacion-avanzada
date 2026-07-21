using proyecto_programacion_avanzada.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyecto_programacion_avanzada.ViewModels.Usuario
{
    public class UsuarioEditViewModel
    {

        //Datos Usuario
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

        // Datos del Residente
        // (solo si el Rol es Residente)


        [Display(Name = "Fecha de ingreso")]
        public DateTime? FechaIngreso { get; set; }

        [Display(Name = "Estado del residente")]
        public EstadoGeneral? EstadoResidente { get; set; }

        [Display(Name = "Vivienda")]
        public int? IdVivienda { get; set; }

        // Lista para el DropDown de Viviendas
        public IEnumerable<SelectListItem> Viviendas { get; set; }

    }
}