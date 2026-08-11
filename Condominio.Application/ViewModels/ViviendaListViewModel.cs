using Condominio.Domain.Enums;

namespace Condominio.Application.ViewModels.Vivienda
{
    public class ViviendaListViewModel
    {
        public int IdVivienda { get; set; }

        public string Numero { get; set; }

        public string Bloque { get; set; }

        public TipoVivienda Tipo { get; set; }

        public EstadoGeneral Estado { get; set; }

        public string TipoNombre => Tipo.ToString();

        public string EstadoNombre => Estado.ToString();
    }
}