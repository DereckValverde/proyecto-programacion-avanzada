using System;
using System.ComponentModel.DataAnnotations;
using Condominio.Domain.Enums;

namespace Condominio.Application.DTOs
{
    public class IncidenciaDto
    {
        public int IdIncidencia { get; set; }

        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required]
        public DateTime FechaReporte { get; set; }

        [Required]
        public EstadoIncidencia Estado { get; set; }

        [Required]
        public PrioridadIncidencia Prioridad { get; set; }

        public string EstadoNombre => Estado.ToString();

        public string PrioridadNombre => Prioridad.ToString();

        public int IdResidente { get; set; }
    }
}