using Condominio.Domain.Enums;

namespace Condominio.Application.ViewModels.Incidencia
{
    public class IncidenciaCreateViewModel
    {
        public string Descripcion { get; set; }

        public PrioridadIncidencia Prioridad { get; set; }

        public int IdResidente { get; set; }
    }
}
