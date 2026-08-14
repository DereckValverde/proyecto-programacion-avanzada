using System;
using System.ComponentModel.DataAnnotations;
using Condominio.Domain.Enums;

namespace Condominio.Application.DTOs
{
    public class VisitanteDto
    {
        public int IdVisitante { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Identificacion { get; set; }

        [Required]
        public TipoVisitante Tipo { get; set; }

        public string TipoNombre => Tipo.ToString();

        [Required]
        public DateTime FechaIngreso { get; set; }

        public DateTime? FechaSalida { get; set; }

        public int IdVivienda { get; set; }

        public string NombreVivienda { get; set; }

    }
}