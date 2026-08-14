using Condominio.Application.Login;
using Condominio.Application.Noticias;
using Condominio.Domain.Noticias;
using System.Collections.Generic;

namespace Condominio.Application.Home
{
    public class HomeIndexViewModel
    {
        public IEnumerable<NoticiaListViewModel> Noticias { get; set; }

        public LoginViewModel Login { get; set; }
    }
}
