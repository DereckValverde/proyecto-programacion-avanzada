using proyecto_programacion_avanzada.ViewModels.Login;
using proyecto_programacion_avanzada.ViewModels.Noticia;
using System.Collections.Generic;

namespace proyecto_programacion_avanzada.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public IEnumerable<NoticiaListViewModel> Noticias { get; set; }

        public LoginViewModel Login { get; set; }
    }
}
