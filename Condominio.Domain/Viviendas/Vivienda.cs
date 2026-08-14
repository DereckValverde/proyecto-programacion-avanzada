using Condominio.Domain.Pagos;
using Condominio.Domain.Reservas;
using Condominio.Domain.Residentes;
using Condominio.Domain.Shared;
using Condominio.Domain.Visitantes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Condominio.Domain.Viviendas
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