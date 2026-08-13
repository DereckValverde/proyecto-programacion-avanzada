using Condominio.Application.ViewModels.Login;
using Condominio.Application.ViewModels.Noticia;
using System.Collections.Generic;

namespace Condominio.Application.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public IEnumerable<NoticiaListViewModel> Noticias { get; set; }

        public LoginViewModel Login { get; set; }
    }
}
