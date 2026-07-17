using System;
using System.ComponentModel.DataAnnotations;

namespace proyecto_programacion_avanzada.DTOs
{
    public class AreaComunDto
    {
        public int IdArea { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(250)]
        public string Descripcion { get; set; }

        [Required]
        public int Capacidad { get; set; }

        [Required]
        public TimeSpan HoraApertura { get; set; }

        [Required]
        public TimeSpan HoraCierre { get; set; }
    }
}