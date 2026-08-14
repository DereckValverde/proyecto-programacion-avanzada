using Condominio.Domain.Incidencias;
using System;

namespace Condominio.Application.Incidencias
{
    public class IncidenciaEditViewModel
    {
        public int IdIncidencia { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaReporte { get; set; }

        public EstadoIncidencia Estado { get; set; }

        public PrioridadIncidencia Prioridad { get; set; }

        public string NombreResidente { get; set; }

        public string NombreVivienda { get; set; }
    }
}
