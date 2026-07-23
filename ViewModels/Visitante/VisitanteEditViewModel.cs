using proyecto_programacion_avanzada.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace proyecto_programacion_avanzada.ViewModels.Visitante
{
    public class VisitanteEditViewModel
    {
        public int IdVisitante { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La identificación es obligatoria.")]
        [StringLength(50)]
        [Display(Name = "Identificación")]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "El tipo es obligatorio.")]
        [Display(Name = "Tipo")]
        public TipoVisitante Tipo { get; set; }

        [Required(ErrorMessage = "La fecha de ingreso es obligatoria.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha y hora de ingreso")]
        public DateTime FechaIngreso { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha y hora de salida")]
        public DateTime? FechaSalida { get; set; }

        [Required(ErrorMessage = "Debe seleccionar la vivienda que visita.")]
        [Display(Name = "Vivienda que visita")]
        public int IdVivienda { get; set; }

        public IEnumerable<SelectListItem> Viviendas { get; set; }
    }
}
