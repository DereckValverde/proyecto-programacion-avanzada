using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Condominio.Domain.Viviendas;

namespace Condominio.Domain.Pagos
{
    [Table("Pagos")]
    public class Pago
    {
        [Key]
        public int IdPago { get; set; }

        [Required]
        public DateTime FechaPago { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal Monto { get; set; }

        [Required]
        [StringLength(20)]
        public string Periodo { get; set; }

        [Required]
        public EstadoPago Estado { get; set; }

        [Required]
        public int IdVivienda { get; set; }

        [ForeignKey("IdVivienda")]
        public virtual Vivienda Vivienda { get; set; }
    }
}