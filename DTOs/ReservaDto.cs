using System;
using System.ComponentModel.DataAnnotations;
using proyecto_programacion_avanzada.Common.Enums;

namespace proyecto_programacion_avanzada.DTOs
{
    public class ReservaDto
    {
        public int IdReserva { get; set; }

        [Required]
        public DateTime FechaReserva { get; set; }

        [Required]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        public TimeSpan HoraFin { get; set; }

        [Required]
        public EstadoReserva Estado { get; set; }

        public int IdVivienda { get; set; }

        public int IdArea { get; set; }
    }
}