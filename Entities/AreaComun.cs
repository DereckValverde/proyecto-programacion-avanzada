using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyecto_programacion_avanzada.Entities
{
    [Table("AreasComunes")]
    public class AreaComun
    {
        [Key]
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

        // Relación
        public virtual ICollection<Reserva> Reservas { get; set; }

        public AreaComun()
        {
            Reservas = new HashSet<Reserva>();
        }
    }
}