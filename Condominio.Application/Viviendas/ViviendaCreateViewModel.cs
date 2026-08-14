using Condominio.Domain.Shared;
using Condominio.Domain.Viviendas;
using System.ComponentModel.DataAnnotations;

namespace Condominio.Application.Viviendas
{
    public class ViviendaCreateViewModel
    {

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