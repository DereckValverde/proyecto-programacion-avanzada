using proyecto_programacion_avanzada.Common.Enums;

namespace proyecto_programacion_avanzada.ViewModels.Vivienda
{
    public class ViviendaListViewModel
    {
        public int IdVivienda { get; set; }

        public string Numero { get; set; }

        public string Bloque { get; set; }

        public TipoVivienda Tipo { get; set; }

        public EstadoGeneral Estado { get; set; }
    }
}