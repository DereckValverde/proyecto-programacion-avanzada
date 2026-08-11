using System;
using System.ComponentModel.DataAnnotations;

namespace Condominio.Application.DTOs
{
    public class NoticiaDto
    {
        public int IdNoticia { get; set; }

        [Required]
        [StringLength(150)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(2000)]
        public string Contenido { get; set; }

        [Required]
        public DateTime FechaPublicacion { get; set; }

        public bool EsAlerta { get; set; }

        [StringLength(100)]
        public string Autor { get; set; }
    }
}
