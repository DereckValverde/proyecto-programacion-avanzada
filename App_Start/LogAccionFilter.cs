using System;
using System.Linq;
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
                var parametros = string.Join(", ", filterContext.ActionParameters
                    .Where(p => p.Value == null || p.Value is string || p.Value.GetType().IsValueType)
                    .Select(p => p.Key + "=" + p.Value));

                Log.Information(
                    "Acción {Verbo} {Controlador}/{Accion} - Usuario: {Usuario} - IP: {Ip} - Parámetros: {Parametros}",
                    solicitud.HttpMethod, controlador, accion, usuario, solicitud.UserHostAddress, parametros);
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "No se pudo registrar la acción.");
            }
        }
    }
}
