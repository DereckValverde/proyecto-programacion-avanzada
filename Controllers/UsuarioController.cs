using proyecto_programacion_avanzada.DTOs;
using proyecto_programacion_avanzada.Infrastructure.DbContexts;
using proyecto_programacion_avanzada.Infrastructure.Repositories.Implementations;
using proyecto_programacion_avanzada.Services.Implementations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyecto_programacion_avanzada.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly UsuarioService _usuarioService;

        public UsuarioController()
        {
            var context = new CondominioContext();

            var repository = new UsuarioRepository(context);

            _usuarioService = new UsuarioService(repository);
            
        }

        public ActionResult Index()
        {
            var usuarios = _usuarioService.ObtenerTodos();

            return View(usuarios);
        }

        //Get: Usuario/Details/n
        public ActionResult Details(int id)
        {
            var usuario = _usuarioService.ObtenerPorId(id);

            if(usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        //Get: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioDto dto)
        {
            if (ModelState.IsValid)
            {
                _usuarioService.Agregar(dto);
                return RedirectToAction("Index");
            }

            return View(dto);
        }

        //Get: Usuario/Edit/n
        public ActionResult Edit(int id)
        {
            var usuario = _usuarioService.ObtenerPorId(id);

            if(usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        //Post: Usuario/Edit/n
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsuarioDto dto)
        {
            if (ModelState.IsValid)
            {
                _usuarioService.Actualizar(dto);
                return RedirectToAction("Index");
            }

            return View(dto);
        }

        //Get: Usuario/Delete/n
        public ActionResult Delete(int id)
        {
            var usuario = _usuarioService.ObtenerPorId(id);

            if(usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        //Post: Usuario/Delete/n
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _usuarioService.Eliminar(id);

            return RedirectToAction("Index");
        }
    }
}
