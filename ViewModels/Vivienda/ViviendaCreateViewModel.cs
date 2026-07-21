using proyecto_programacion_avanzada.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace proyecto_programacion_avanzada.ViewModels.Vivienda
{
    public class ViviendaCreateViewModel
    {

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
    }
}