using Condominio.Domain.Enums;
using System;

namespace Condominio.Application.ViewModels.Reserva
{
    public class ReservaDetailsViewModel
    {
        public int IdReserva { get; set; }
        public DateTime FechaReserva { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public EstadoReserva Estado { get; set; }
        public string NombreVivienda { get; set; }
        public string NombreArea { get; set; }
    }
}