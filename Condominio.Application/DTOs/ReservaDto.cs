using System;
using System.ComponentModel.DataAnnotations;
using Condominio.Domain.Enums;

namespace Condominio.Application.DTOs
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

        public string EstadoNombre => Estado.ToString();

        public int IdVivienda { get; set; }

        public int IdArea { get; set; }

        public string NombreVivienda { get; set; }

        public string NombreArea { get; set; }
    }
}