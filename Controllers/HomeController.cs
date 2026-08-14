using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web;
using Condominio.Identity;
using Condominio.Application.Home;
using Condominio.Application.Login;
using Condominio.Application.Mappings;
using Condominio.Application.Noticias;
using Condominio.Domain.Noticias;
using Condominio.Domain.Shared;
using Condominio.Infrastructure.DbContexts;
using Condominio.Infrastructure.Repositories;
using Serilog;

namespace Condominio.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly CondominioContext _context;
        private readonly INoticiaService _noticiaService;

        public HomeController()
        {
            _context = new CondominioContext();
            _noticiaService = new NoticiaService(new NoticiaRepository(_context));
        }

        private ApplicationUserManager UserManager
        {
            get { return ApplicationUserManager.Create(_context); }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        private HomeIndexViewModel CargarModeloIndex(LoginViewModel login = null)
        {
            var noticias = _noticiaService.ObtenerTodas();

            return new HomeIndexViewModel
            {
                Noticias = AutoMapperConfig.Mapper.Map<IEnumerable<NoticiaListViewModel>>(noticias),
                Login = login ?? new LoginViewModel()
            };
        }

        public ActionResult Index()
        {
            return View(CargarModeloIndex());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login([Bind(Prefix = "Login")] LoginViewModel model)
        {
            model = model ?? new LoginViewModel();

            if (!ModelState.IsValid)
            {
                return View("Index", CargarModeloIndex(model));
            }

            TryValidateModel(model, "Login");

            if (!ModelState.IsValid)
            {
                return View("Index", CargarModeloIndex(model));
            }

            var userManager = UserManager;
            var usuario = await userManager.FindByNameAsync(model.Correo);

            if (usuario == null || usuario.Estado != EstadoGeneral.Activo)
            {
                Log.Warning("Intento de inicio de sesión fallido - Correo: {Correo} - IP: {Ip} - Motivo: correo no encontrado o cuenta inactiva",
                    model.Correo, Request.UserHostAddress);

                ModelState.AddModelError(string.Empty, "Correo electrónico o contraseña incorrectos.");
                return View("Index", CargarModeloIndex(model));
            }

            PasswordVerificationResult verificacion = PasswordVerificationResult.Failed;

            if (usuario.PasswordHash == model.Contrasena)
            {
                usuario.PasswordHash = userManager.PasswordHasher.HashPassword(model.Contrasena);
                usuario.SecurityStamp = Guid.NewGuid().ToString();
                userManager.Update(usuario);
                verificacion = PasswordVerificationResult.Success;
            }
            else
            {
                try
                {
                    verificacion = userManager.PasswordHasher
                        .VerifyHashedPassword(usuario.PasswordHash, model.Contrasena);
                }
                catch (FormatException)
                {
                    verificacion = PasswordVerificationResult.Failed;
                }
            }

            if (verificacion == PasswordVerificationResult.Failed)
            {
                Log.Warning("Intento de inicio de sesión fallido - Correo: {Correo} - IP: {Ip} - Motivo: contraseña incorrecta",
                    model.Correo, Request.UserHostAddress);

                ModelState.AddModelError(string.Empty, "Correo electrónico o contraseña incorrectos.");
                return View("Index", CargarModeloIndex(model));
            }

            if (usuario.SecurityStamp == null)
            {
                usuario.SecurityStamp = Guid.NewGuid().ToString();
                userManager.Update(usuario);
            }

            var rol = usuario.Rol.ToString();

            ApplicationUserManager.AsegurarRol(_context, rol);

            if (!userManager.IsInRole(usuario.Id, rol))
            {
                userManager.AddToRole(usuario.Id, rol);
            }

            var identity = await userManager.CreateIdentityAsync(usuario, DefaultAuthenticationTypes.ApplicationCookie);
            identity.AddClaim(new Claim("Nombre", usuario.Nombre));

            AuthenticationManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties
            {
                IsPersistent = false
            }, identity);

            Log.Information("Inicio de sesión exitoso - Usuario: {Correo} - Rol: {Rol} - IP: {Ip}",
                model.Correo, rol, Request.UserHostAddress);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Log.Information("Cierre de sesión - Usuario: {Usuario} - IP: {Ip}",
                User.Identity.Name, Request.UserHostAddress);

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
