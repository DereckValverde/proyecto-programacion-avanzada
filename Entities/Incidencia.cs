using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using proyecto_programacion_avanzada.Common.Enums;

namespace proyecto_programacion_avanzada.Entities
{
    [Table("Incidencias")]
    public class Incidencia
    {
        [Key]
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

        [Required]
        public int IdResidente { get; set; }

        [ForeignKey("IdResidente")]
        public virtual Residente Residente { get; set; }
    }
}