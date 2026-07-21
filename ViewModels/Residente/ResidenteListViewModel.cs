using proyecto_programacion_avanzada.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyecto_programacion_avanzada.ViewModels.Residente
{
    public class ResidenteListViewModel
    {
        public int IdResidente { get; set; }

        public string Nombre { get; set; }

        public DateTime FechaIngreso { get; set; }

        public EstadoGeneral Estado { get; set; }

        public int IdUsuario { get; set; }

        public int IdVivienda { get; set; }

        public string NombreUsuario { get; set; }

        public string Vivienda { get; set; }
    }
}