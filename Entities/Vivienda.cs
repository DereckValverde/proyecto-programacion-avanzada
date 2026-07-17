using proyecto_programacion_avanzada.Common.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyecto_programacion_avanzada.Entities
{
    [Table("Viviendas")]
    public class Vivienda
    {
        [Key]
        public int IdVivienda { get; set; }

        [Required]
        [StringLength(20)]
        public string Numero { get; set; }

        [Required]
        [StringLength(20)]
        public string Bloque { get; set; }

        [Required]
        public TipoVivienda Tipo { get; set; }

        [Required]
        public EstadoGeneral Estado { get; set; }

        // Relaciones
        public virtual ICollection<Residente> Residentes { get; set; }

        public virtual ICollection<Pago> Pagos { get; set; }

        public virtual ICollection<Reserva> Reservas { get; set; }

        public virtual ICollection<Visitante> Visitantes { get; set; }

        public Vivienda()
        {
            Residentes = new HashSet<Residente>();
            Pagos = new HashSet<Pago>();
            Reservas = new HashSet<Reserva>();
            Visitantes = new HashSet<Visitante>();
        }
    }
}