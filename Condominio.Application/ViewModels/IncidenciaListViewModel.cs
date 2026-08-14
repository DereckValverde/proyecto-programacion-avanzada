using Condominio.Domain.Enums;
using System;

namespace Condominio.Application.ViewModels.Incidencia
{
    public class IncidenciaListViewModel
    {
        public int IdIncidencia { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaReporte { get; set; }

        public EstadoIncidencia Estado { get; set; }

        public string EstadoNombre => Estado.ToString();

        public PrioridadIncidencia Prioridad { get; set; }

        public string PrioridadNombre => Prioridad.ToString();

        public string NombreResidente { get; set; }

        public string NombreVivienda { get; set; }
    }
}
