using Condominio.Domain.Pagos;
using Condominio.Domain.Viviendas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Condominio.Application.Pagos
{
    public class PagoEditViewModel
    {
        public int IdPago { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de pago")]
        public DateTime FechaPago { get; set; }

        [Display(Name = "Monto")]
        public decimal Monto { get; set; }

        [Display(Name = "Periodo (AAAA-MM)")]
        public string Periodo { get; set; }

        [Display(Name = "Estado")]
        public EstadoPago Estado { get; set; }

        [Display(Name = "Vivienda")]
        public int IdVivienda { get; set; }

        public IEnumerable<SelectListItem> Viviendas { get; set; }
    }
}
