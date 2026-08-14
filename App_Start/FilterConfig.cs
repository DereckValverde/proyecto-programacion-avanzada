using System.Web;
using System.Web.Mvc;
using Condominio.Web.App_Start;

namespace Condominio.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LogAccionFilter());
        }
    }
}
