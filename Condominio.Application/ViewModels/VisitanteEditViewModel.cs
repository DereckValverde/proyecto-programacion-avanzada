using Condominio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Condominio.Application.ViewModels.Visitante
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

        [Display(Name = "Tipo")]
        public TipoVisitante Tipo { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha y hora de ingreso")]
        public DateTime FechaIngreso { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha y hora de salida")]
        public DateTime? FechaSalida { get; set; }

        [Display(Name = "Vivienda que visita")]
        public int IdVivienda { get; set; }

        public IEnumerable<SelectListItem> Viviendas { get; set; }
    }
}
