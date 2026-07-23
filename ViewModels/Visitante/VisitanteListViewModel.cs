using proyecto_programacion_avanzada.Common.Enums;
using System;

namespace proyecto_programacion_avanzada.ViewModels.Visitante
{
    public class VisitanteListViewModel
    {
        public int IdVisitante { get; set; }

        public string Nombre { get; set; }

        public string Identificacion { get; set; }

        public TipoVisitante Tipo { get; set; }

        public DateTime FechaIngreso { get; set; }

        public DateTime? FechaSalida { get; set; }

        public bool SeEncuentraDentro => FechaSalida == null;

        public int IdVivienda { get; set; }

        public string NombreVivienda { get; set; }
    }
}
