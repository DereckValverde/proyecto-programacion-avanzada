using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using proyecto_programacion_avanzada.App_Start;
using proyecto_programacion_avanzada.Mappings;

namespace proyecto_programacion_avanzada
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            FluentValidationConfig.RegisterFluentValidation();

            AutoMapperConfig.RegisterMappings();
        }
    }
}
