using Condominio.Domain.Visitantes;
using Condominio.Domain.Viviendas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Condominio.Application.Visitantes
{
    public class VisitanteIngresoViewModel
    {
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

        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha y hora de ingreso")]
        public DateTime FechaIngreso { get; set; } = DateTime.Now;

        [Display(Name = "Vivienda que visita")]
        public int IdVivienda { get; set; }

        public IEnumerable<SelectListItem> Viviendas { get; set; }
    }
}
