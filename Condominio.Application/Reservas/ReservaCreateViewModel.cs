using Condominio.Domain.Reservas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Condominio.Application.Reservas
{
    public class ReservaCreateViewModel
    {
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