using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Condominio.Web.App_Start;
using Condominio.Application.Mappings;
using Serilog;

namespace Condominio.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            SerilogConfig.Configurar();

            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            FluentValidationConfig.RegisterFluentValidation();

            AutoMapperConfig.RegisterMappings();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var excepcion = Server.GetLastError();

            if (excepcion != null)
            {
                Log.Error(excepcion, "Excepción no controlada en la aplicación.");
            }
        }

        protected void Application_End()
        {
            Log.CloseAndFlush();
        }
    }
}
