using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Condominio.Identity;
using Condominio.Infrastructure.DbContexts;
using Condominio.Infrastructure.Migrations;

[assembly: OwinStartup(typeof(Condominio.Web.Startup))]

namespace Condominio.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<CondominioContext>(() => new CondominioContext());

            app.CreatePerOwinContext<ApplicationUserManager>(
                (options, context) => ApplicationUserManager.Create(context.Get<CondominioContext>()));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Index"),
                CookieName = "CondominioCookie",
                CookieHttpOnly = true
            });
        }
    }
}
