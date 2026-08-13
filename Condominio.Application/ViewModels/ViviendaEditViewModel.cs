using System.ComponentModel.DataAnnotations;
using Condominio.Domain.Enums;

namespace Condominio.Application.ViewModels.Vivienda
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