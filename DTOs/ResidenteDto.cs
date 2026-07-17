using System;
using System.ComponentModel.DataAnnotations;
using proyecto_programacion_avanzada.Common.Enums;

namespace proyecto_programacion_avanzada.DTOs
{
    public class ResidenteDto
    {
        public int IdResidente { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        public DateTime FechaIngreso { get; set; }

        [Required]
        public EstadoGeneral Estado { get; set; }

        public int IdUsuario { get; set; }

        public int IdVivienda { get; set; }
    }
}