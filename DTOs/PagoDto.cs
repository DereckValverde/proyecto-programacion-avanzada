using System;
using System.ComponentModel.DataAnnotations;
using proyecto_programacion_avanzada.Common.Enums;

namespace proyecto_programacion_avanzada.DTOs
{
    public class PagoDto
    {
        public int IdPago { get; set; }

        [Required]
        public DateTime FechaPago { get; set; }

        [Required]
        public decimal Monto { get; set; }

        [Required]
        [StringLength(20)]
        public string Periodo { get; set; }

        [Required]
        public EstadoPago Estado { get; set; }

        public int IdVivienda { get; set; }
    }
}