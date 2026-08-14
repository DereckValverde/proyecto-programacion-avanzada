using System.ComponentModel.DataAnnotations;

namespace Condominio.Application.Noticias
{
    public class NoticiaCreateViewModel
    {
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Display(Name = "Contenido")]
        public string Contenido { get; set; }

        [Display(Name = "Mostrar como alerta")]
        public bool EsAlerta { get; set; }

        [Display(Name = "Autor")]
        public string Autor { get; set; }
    }
}
