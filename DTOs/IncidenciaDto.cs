using System;
using System.ComponentModel.DataAnnotations;
using proyecto_programacion_avanzada.Common.Enums;

namespace proyecto_programacion_avanzada.DTOs
{
    public class IncidenciaDto
    {
        public int IdIncidencia { get; set; }

        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required]
        public DateTime FechaReporte { get; set; }

        [Required]
        public EstadoIncidencia Estado { get; set; }

        [Required]
        public PrioridadIncidencia Prioridad { get; set; }

        public int IdResidente { get; set; }
    }
}