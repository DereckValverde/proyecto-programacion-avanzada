using proyecto_programacion_avanzada.DTOs;
using proyecto_programacion_avanzada.Infrastructure.DbContexts;
using proyecto_programacion_avanzada.Infrastructure.Repositories.Implementations;
using proyecto_programacion_avanzada.Services.Implementations;
using AutoMapper;
using proyecto_programacion_avanzada.ViewModels;
using System.Web.Mvc;
using proyecto_programacion_avanzada.Mappings;
using System.Collections.Generic;
using System.Web.Management;
using System.Data.SqlTypes;

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
            var usuariosDto = _usuarioService.ObtenerTodos();

            var usuario = AutoMapperConfig.Mapper.Map<IEnumerable<UsuarioListViewModel>>(usuariosDto);


            return View(usuario);
        }

        //Get: Usuario/Details/n
        public ActionResult Details(int id)
        {
            var usuarioDto = _usuarioService.ObtenerPorId(id);

            if(usuarioDto == null)
            {
                return HttpNotFound();
            }

            var usuario = AutoMapperConfig.Mapper.Map<UsuarioDetailsViewModel>(usuarioDto);

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
        public ActionResult Create(UsuarioCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = AutoMapperConfig.Mapper.Map<UsuarioDto>(model);

                _usuarioService.Agregar(dto);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        //Get: Usuario/Edit/n
        public ActionResult Edit(int id)
        {
            var usuarioDto = _usuarioService.ObtenerPorId(id);

            if(usuarioDto == null)
            {
                return HttpNotFound();
            }

            var model = AutoMapperConfig.Mapper.Map<UsuarioEditViewModel>(usuarioDto);

            return View(model);
        }

        //Post: Usuario/Edit/n
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsuarioEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = AutoMapperConfig.Mapper.Map<UsuarioDto>(model);

                _usuarioService.Actualizar(dto);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        //Get: Usuario/Delete/n
        public ActionResult Delete(int id)
        {
            var usuarioDto = _usuarioService.ObtenerPorId(id);

            if (usuarioDto == null)
            {
                return HttpNotFound();
            }

            var model = AutoMapperConfig.Mapper.Map<UsuarioDetailsViewModel>(usuarioDto);

            return View(model);
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
