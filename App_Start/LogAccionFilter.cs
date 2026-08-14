using System;
using System.Web.Mvc;
using Serilog;

namespace Condominio.Web.App_Start
{
    public class LogAccionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var solicitud = filterContext.HttpContext.Request;
                var usuario = filterContext.HttpContext.User.Identity.Name ?? "Anónimo";
                var controlador = filterContext.Controller.GetType().Name;
                var accion = filterContext.ActionDescriptor.ActionName;

                Log.Information(
                    "Acción {Verbo} {Controlador}/{Accion} - Usuario: {Usuario}",
                    solicitud.HttpMethod, controlador, accion, usuario);
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "No se pudo registrar la acción.");
            }
        }
    }
}
