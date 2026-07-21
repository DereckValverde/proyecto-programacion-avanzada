using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using proyecto_programacion_avanzada.Common.Enums;

namespace proyecto_programacion_avanzada.Entities
{
    [Table("Residentes")]
    public class Residente
    {
        [Key]
        public int IdResidente { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        public DateTime FechaIngreso { get; set; }

        [Required]
        public EstadoGeneral Estado { get; set; }

        [Required]
        [Index("IX_Residente_IdUsuario", IsUnique = true)]
        public int IdUsuario { get; set; }

        [Required]
        public int IdVivienda { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }

        [ForeignKey("IdVivienda")]
        public virtual Vivienda Vivienda { get; set; }

        public virtual ICollection<Incidencia> Incidencias { get; set; }

        public Residente()
        {
            Incidencias = new HashSet<Incidencia>();
        }
    }
}