using Condominio.Domain.Shared;
using Condominio.Domain.Viviendas;

namespace Condominio.Application.Viviendas
{
    public class ViviendaDetailsViewModel
    {
        public int IdVivienda { get; set; }

        public string Numero { get; set; }

        public string Bloque { get; set; }

        public TipoVivienda Tipo { get; set; }

        public EstadoGeneral Estado { get; set; }
    }
}