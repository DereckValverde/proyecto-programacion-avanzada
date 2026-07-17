using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using proyecto_programacion_avanzada.Common.Enums;

namespace proyecto_programacion_avanzada.Entities
{
    [Table("Visitantes")]
    public class Visitante
    {
        [Key]
        public int IdVisitante { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        [Index("IX_Visitante_Identificacion", IsUnique = true)]
        public string Identificacion { get; set; }

        [Required]
        public TipoVisitante Tipo { get; set; }

        [Required]
        public DateTime FechaIngreso { get; set; }

        public DateTime? FechaSalida { get; set; }

        [Required]
        public int IdVivienda { get; set; }

        [ForeignKey("IdVivienda")]
        public virtual Vivienda Vivienda { get; set; }
    }
}