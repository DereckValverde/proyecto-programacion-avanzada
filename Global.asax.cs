using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using proyecto_programacion_avanzada.Common.Enums;
using proyecto_programacion_avanzada.Entities;
using proyecto_programacion_avanzada.Infrastructure.DbContexts;
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

            AutoMapperConfig.RegisterMappings();

            CrearUsuarioAdministradorInicial();
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie == null)
            {
                return;
            }

            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            if (authTicket == null)
            {
                return;
            }

            var roles = authTicket.UserData.Split(';');
            var identity = new FormsIdentity(authTicket);
            var principal = new GenericPrincipal(identity, roles);

            HttpContext.Current.User = principal;
        }

        private void CrearUsuarioAdministradorInicial()
        {
            using (var context = new CondominioContext())
            {
                if (context.Usuarios.Any())
                {
                    return;
                }

                context.Usuarios.Add(new Usuario
                {
                    Nombre = "Administrador General",
                    Correo = "admin@condominio.com",
                    Telefono = "00000000",
                    Contrasena = "Admin123",
                    Rol = RolUsuario.Administrador,
                    Estado = EstadoGeneral.Activo
                });

                context.SaveChanges();
            }
        }
    }
}
