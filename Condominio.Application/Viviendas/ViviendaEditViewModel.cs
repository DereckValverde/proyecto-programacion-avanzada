using System.ComponentModel.DataAnnotations;
using Condominio.Domain.Shared;
using Condominio.Domain.Viviendas;

namespace Condominio.Application.Viviendas
{
    public class ViviendaEditViewModel
    {
        public int IdVivienda { get; set; }

        [Required]
        [StringLength(20)]
        public string Numero { get; set; }

        [Required]
        [StringLength(20)]
        public string Bloque { get; set; }

        public TipoVivienda Tipo { get; set; }

        public EstadoGeneral Estado { get; set; }
    }
}