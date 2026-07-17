using System;
using System.ComponentModel.DataAnnotations;
using proyecto_programacion_avanzada.Common.Enums;

namespace proyecto_programacion_avanzada.DTOs
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

        [Required]
        public DateTime FechaIngreso { get; set; }

        public DateTime? FechaSalida { get; set; }

        public int IdVivienda { get; set; }
    }
}