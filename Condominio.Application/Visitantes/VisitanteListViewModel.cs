using Condominio.Domain.Visitantes;
using System;

namespace Condominio.Application.Visitantes
{
    public class VisitanteListViewModel
    {
        public int IdVisitante { get; set; }

        public string Nombre { get; set; }

        public string Identificacion { get; set; }

        public TipoVisitante Tipo { get; set; }

        public string TipoNombre => Tipo.ToString();

        public DateTime FechaIngreso { get; set; }

        public DateTime? FechaSalida { get; set; }

        public bool SeEncuentraDentro => FechaSalida == null;

        public int IdVivienda { get; set; }

        public string NombreVivienda { get; set; }
    }
}
