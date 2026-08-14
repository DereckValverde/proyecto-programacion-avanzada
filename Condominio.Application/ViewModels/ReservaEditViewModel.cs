using Condominio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Condominio.Application.ViewModels.Reserva
{
    public class ReservaEditViewModel
    {
        public int IdReserva { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaReserva { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraFin { get; set; }

        public EstadoReserva Estado { get; set; }

        public int IdVivienda { get; set; }

        public int IdArea { get; set; }

        public IEnumerable<SelectListItem> Viviendas { get; set; }

        public IEnumerable<SelectListItem> AreasComunes { get; set; }
    }
}