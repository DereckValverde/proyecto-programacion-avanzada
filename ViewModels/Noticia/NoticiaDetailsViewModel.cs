using System;

namespace proyecto_programacion_avanzada.ViewModels.Noticia
{
    public class NoticiaDetailsViewModel
    {
        public int IdNoticia { get; set; }

        public string Titulo { get; set; }

        public string Contenido { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public bool EsAlerta { get; set; }

        public string Autor { get; set; }
    }
}
