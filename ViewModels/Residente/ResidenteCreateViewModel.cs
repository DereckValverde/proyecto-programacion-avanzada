using proyecto_programacion_avanzada.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace proyecto_programacion_avanzada.ViewModels.Residente
{
    public class ResidenteCreateViewModel
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; }

        [Required]
        public EstadoGeneral Estado { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        [Required]
        public int IdVivienda { get; set; }

        public IEnumerable<SelectListItem> Usuarios { get; set; }

        public IEnumerable<SelectListItem> Viviendas { get; set; }
    }
}