using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using proyecto_programacion_avanzada.Common.Enums;
using proyecto_programacion_avanzada.Infrastructure.DbContexts;
using proyecto_programacion_avanzada.Models.ViewModels;

namespace proyecto_programacion_avanzada.Controllers
{
    public class AccountController : Controller
    {
        private readonly CondominioContext _context = new CondominioContext();

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Correo == model.Correo && u.Contrasena == model.Contrasena);

            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
                return View(model);
            }

            if (usuario.Estado != EstadoGeneral.Activo)
            {
                ModelState.AddModelError(string.Empty, "El usuario se encuentra inactivo. Contacte a la administración.");
                return View(model);
            }

            var ticket = new FormsAuthenticationTicket(
                1,
                usuario.Correo,
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                model.RememberMe,
                usuario.Rol.ToString(),
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            if (model.RememberMe)
            {
                authCookie.Expires = ticket.Expiration;
            }

            Response.Cookies.Add(authCookie);

            Session["IdUsuario"] = usuario.IdUsuario;
            Session["NombreUsuario"] = usuario.Nombre;

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
