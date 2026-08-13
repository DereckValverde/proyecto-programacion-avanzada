using System.ComponentModel.DataAnnotations;
using Condominio.Domain.Enums;

namespace Condominio.Application.DTOs
{
    public class ViviendaDto
    {
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

        public string TipoNombre => Tipo.ToString();

        public string EstadoNombre => Estado.ToString();
    }
}