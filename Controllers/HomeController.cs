using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyecto_programacion_avanzada.Common.Enums;


namespace proyecto_programacion_avanzada.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole(RolUsuario.Administrador.ToString()))
            {
                return RedirectToAction(nameof(AdminDashboard));
            }

            if (User.IsInRole(RolUsuario.Residente.ToString()))
            {
                return RedirectToAction(nameof(ResidenteDashboard));
            }

            if (User.IsInRole(RolUsuario.Guarda.ToString()))
            {
                return RedirectToAction(nameof(GuardaDashboard));
            }

            return View();
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult AdminDashboard()
        {
            return View();
        }

        [Authorize(Roles = "Residente")]
        public ActionResult ResidenteDashboard()
        {
            return View();
        }

        [Authorize(Roles = "Guarda")]
        public ActionResult GuardaDashboard()
        {
            return View();
        }
    }
}

