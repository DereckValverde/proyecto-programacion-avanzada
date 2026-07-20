using System.ComponentModel.DataAnnotations;
using proyecto_programacion_avanzada.Common.Enums;

namespace proyecto_programacion_avanzada.ViewModels.Vivienda
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

        [Required]
        public TipoVivienda Tipo { get; set; }

        [Required]
        public EstadoGeneral Estado { get; set; }
    }
}