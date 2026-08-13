using Condominio.Application.DTOs;
using Condominio.Infrastructure.DbContexts;
using Condominio.Infrastructure.Repositories.Implementations;
using Condominio.Application.Mappings;
using Condominio.Application.Services.Implementations;
using Condominio.Application.ViewModels.Reserva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Condominio.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ReservaController : Controller
    {
        private readonly ReservaService _reservaService;
        private readonly ViviendaService _viviendaService;
        private readonly AreaComunService _areaComunService;

        public ReservaController()
        {
            var context = new CondominioContext();

            var reservaRepository = new ReservaRepository(context);
            var viviendaRepository = new ViviendaRepository(context);
            var areaComunRepository = new AreaComunRepository(context);

            _reservaService = new ReservaService(reservaRepository);
            _viviendaService = new ViviendaService(viviendaRepository);
            _areaComunService = new AreaComunService(areaComunRepository);
        }

        private void CargarCombos()
        {
            ViewBag.Viviendas = _viviendaService
                .obtenerTodos()
                .Select(v => new SelectListItem
                {
                    Value = v.IdVivienda.ToString(),
                    Text = "Bloque " + v.Bloque + " - Vivienda " + v.Numero
                })
                .ToList();

            ViewBag.AreasComunes = _areaComunService
                .ObtenerTodas()
                .Select(a => new SelectListItem
                {
                    Value = a.IdArea.ToString(),
                    Text = a.Nombre
                })
                .ToList();
        }

        // GET: Reserva
        public ActionResult Index()
        {
            var reservasDto = _reservaService.ObtenerTodos();

            var reservas = AutoMapperConfig.Mapper.Map<IEnumerable<ReservaListViewModel>>(reservasDto);

            return View(reservas);
        }

        // GET: Reserva/Details/5
        public ActionResult Details(int id)
        {
            var reservaDto = _reservaService.ObtenerPorId(id);

            if (reservaDto == null)
                return HttpNotFound();

            var model = AutoMapperConfig.Mapper.Map<ReservaDetailsViewModel>(reservaDto);

            return View(model);
        }

        // GET: Reserva/Create
        public ActionResult Create()
        {
            CargarCombos();

            return View(new ReservaCreateViewModel());
        }

        // POST: Reserva/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReservaCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dto = AutoMapperConfig.Mapper.Map<ReservaDto>(model);

                    _reservaService.Agregar(dto);

                    TempData["Success"] = "Reserva registrada exitosamente.";

                    return RedirectToAction("Index");
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            CargarCombos();

            return View(model);
        }

        // GET: Reserva/Edit/5
        public ActionResult Edit(int id)
        {
            var reservaDto = _reservaService.ObtenerPorId(id);

            if (reservaDto == null)
                return HttpNotFound();

            var model = AutoMapperConfig.Mapper.Map<ReservaEditViewModel>(reservaDto);

            CargarCombos();

            return View(model);
        }

        // POST: Reserva/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReservaEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dto = AutoMapperConfig.Mapper.Map<ReservaDto>(model);

                    _reservaService.Actualizar(dto);

                    TempData["Success"] = "Reserva actualizada exitosamente.";

                    return RedirectToAction("Index");
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            CargarCombos();

            return View(model);
        }

        // GET: Reserva/Delete/5
        public ActionResult Delete(int id)
        {
            var reservaDto = _reservaService.ObtenerPorId(id);

            if (reservaDto == null)
                return HttpNotFound();

            var model = AutoMapperConfig.Mapper.Map<ReservaDetailsViewModel>(reservaDto);

            return View(model);
        }

        // POST: Reserva/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _reservaService.Eliminar(id);

            TempData["Success"] = "Reserva eliminada exitosamente.";

            return RedirectToAction("Index");
        }
    }
}