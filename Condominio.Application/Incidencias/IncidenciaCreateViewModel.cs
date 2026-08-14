using Condominio.Domain.Incidencias;

namespace Condominio.Application.Incidencias
{
    public class IncidenciaCreateViewModel
    {
        public string Descripcion { get; set; }

        public PrioridadIncidencia Prioridad { get; set; }

        public int IdResidente { get; set; }
    }
}
