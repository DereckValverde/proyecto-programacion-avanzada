using Condominio.Domain.Shared;
using Condominio.Domain.Viviendas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Condominio.Application.Residentes
{
    public class ResidenteListViewModel
    {
        public int IdResidente { get; set; }

        public string Nombre { get; set; }

        public DateTime FechaIngreso { get; set; }

        public EstadoGeneral Estado { get; set; }

        public string EstadoNombre => Estado.ToString();

        public int IdUsuario { get; set; }

        public int IdVivienda { get; set; }

        public string NombreUsuario { get; set; }

        public string Vivienda { get; set; }
    }
}