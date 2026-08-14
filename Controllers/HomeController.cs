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
using Condominio.Domain.Enums;
using Condominio.Infrastructure.DbContexts;
using Condominio.Infrastructure.Repositories.Implementations;
using Condominio.Application.Mappings;
using Condominio.Application.Services.Implementations;
using Condominio.Application.Services.Interfaces;
using Condominio.Application.ViewModels.Home;
using Condominio.Application.ViewModels.Login;
using Condominio.Application.ViewModels.Noticia;

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

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
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
