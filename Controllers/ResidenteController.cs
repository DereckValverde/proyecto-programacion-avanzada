using AutoMapper;
using proyecto_programacion_avanzada.DTOs;
using proyecto_programacion_avanzada.Infrastructure.DbContexts;
using proyecto_programacion_avanzada.Infrastructure.Repositories.Implementations;
using proyecto_programacion_avanzada.Mappings;
using proyecto_programacion_avanzada.Services.Implementations;
using proyecto_programacion_avanzada.ViewModels;
using proyecto_programacion_avanzada.ViewModels.Residente;
using System.Collections.Generic;
using System.Web.Mvc;

namespace proyecto_programacion_avanzada.Controllers
{
    public class ResidenteController : Controller
    {
        private readonly ResidenteService _residenteService;
        private readonly UsuarioService _usuarioService;
        private readonly ViviendaService _viviendaService;

        public ResidenteController()
        {
            var context = new CondominioContext();

            var residenteRepository = new ResidenteRepository(context);
            var usuarioRepository = new UsuarioRepository(context);
            var viviendaRepository = new ViviendaRepository(context);

            _residenteService = new ResidenteService(residenteRepository);
            _usuarioService = new UsuarioService(usuarioRepository);
            _viviendaService = new ViviendaService(viviendaRepository);
        }

        private void CargarCombos()
        {
            ViewBag.Usuarios = new SelectList(
                _usuarioService.ObtenerTodos(),
                "IdUsuario",
                "Correo"
            );

            ViewBag.Viviendas = new SelectList(
                _viviendaService.obtenerTodos(),
                "IdVivienda",
                "Numero"
            );
        }

        // GET: Residente
        public ActionResult Index()
        {
            var residentesDto = _residenteService.ObtenerTodos();

            var residentes = AutoMapperConfig.Mapper.Map<IEnumerable<ResidenteListViewModel>>(residentesDto);

            return View(residentes);
        }

        // GET: Residente/Details/5
        public ActionResult Details(int id)
        {
            var residenteDto = _residenteService.ObtenerPorId(id);

            if (residenteDto == null)
                return HttpNotFound();

            var model = AutoMapperConfig.Mapper.Map<ResidenteDetailsViewModel>(residenteDto);

            return View(model);
        }

        // GET: Residente/Create
        public ActionResult Create()
        {
            CargarCombos();

            return View();
        }

        // POST: Residente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ResidenteCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = AutoMapperConfig.Mapper.Map<ResidenteDto>(model);

                _residenteService.Agregar(dto);

                return RedirectToAction("Index");
            }

            CargarCombos();

            return View(model);
        }

        // GET: Residente/Edit/5
        public ActionResult Edit(int id)
        {
            var residenteDto = _residenteService.ObtenerPorId(id);

            if (residenteDto == null)
                return HttpNotFound();

            var model = AutoMapperConfig.Mapper.Map<ResidenteEditViewModel>(residenteDto);

            CargarCombos();

            return View(model);
        }

        // POST: Residente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ResidenteEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = AutoMapperConfig.Mapper.Map<ResidenteDto>(model);

                _residenteService.Actualizar(dto);

                return RedirectToAction("Index");
            }

            CargarCombos();

            return View(model);
        }

        // GET: Residente/Delete/n
        public ActionResult Delete(int id)
        {
            var residenteDto = _residenteService.ObtenerPorId(id);

            if (residenteDto == null)
                return HttpNotFound();

            var model = AutoMapperConfig.Mapper.Map<ResidenteDetailsViewModel>(residenteDto);

            return View(model);
        }

        // POST: Residente/Delete/n
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _residenteService.Eliminar(id);

            return RedirectToAction("Index");
        }
    }
}