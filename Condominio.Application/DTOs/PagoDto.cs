using System;
using System.ComponentModel.DataAnnotations;
using Condominio.Domain.Enums;

namespace Condominio.Application.DTOs
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

        public string EstadoNombre => Estado.ToString();

        public int IdVivienda { get; set; }

        public string NombreVivienda { get; set; }
    }
}