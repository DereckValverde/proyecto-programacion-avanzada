using Condominio.Domain.Enums;
using System;

namespace Condominio.Application.ViewModels.Pago
{
    public class PagoListViewModel
    {
        public int IdPago { get; set; }

        public DateTime FechaPago { get; set; }

        public decimal Monto { get; set; }

        public string Periodo { get; set; }

        public EstadoPago Estado { get; set; }

        public string EstadoNombre => Estado.ToString();

        public int IdVivienda { get; set; }

        public string NombreVivienda { get; set; }
    }
}
