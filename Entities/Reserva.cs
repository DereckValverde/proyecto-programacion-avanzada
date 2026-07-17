using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using proyecto_programacion_avanzada.Common.Enums;

namespace proyecto_programacion_avanzada.Entities
{
    [Table("Reservas")]
    public class Reserva
    {
        [Key]
        public int IdReserva { get; set; }

        [Required]
        public DateTime FechaReserva { get; set; }

        [Required]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        public TimeSpan HoraFin { get; set; }

        [Required]
        public EstadoReserva Estado { get; set; }

        [Required]
        public int IdVivienda { get; set; }

        [Required]
        public int IdArea { get; set; }

        [ForeignKey("IdVivienda")]
        public virtual Vivienda Vivienda { get; set; }

        [ForeignKey("IdArea")]
        public virtual AreaComun AreaComun { get; set; }
    }
}