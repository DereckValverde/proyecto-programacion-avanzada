using Condominio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Condominio.Application.ViewModels.Residente
{
    public class ResidenteCreateViewModel
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; }

        public EstadoGeneral Estado { get; set; }

        public int IdUsuario { get; set; }

        public int IdVivienda { get; set; }

        public IEnumerable<SelectListItem> Usuarios { get; set; }

        public IEnumerable<SelectListItem> Viviendas { get; set; }
    }
}