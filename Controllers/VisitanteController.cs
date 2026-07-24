using AutoMapper;
using proyecto_programacion_avanzada.DTOs;
using proyecto_programacion_avanzada.Infrastructure.DbContexts;
using proyecto_programacion_avanzada.Infrastructure.Repositories.Implementations;
using proyecto_programacion_avanzada.Mappings;
using proyecto_programacion_avanzada.Services.Implementations;
using proyecto_programacion_avanzada.ViewModels.Visitante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace proyecto_programacion_avanzada.Controllers
{
    public class VisitanteController : Controller
    {
        private readonly VisitanteService _visitanteService;
        private readonly ViviendaService _viviendaService;

        public VisitanteController()
        {
            var context = new CondominioContext();

            var visitanteRepository = new VisitanteRepository(context);
            var viviendaRepository = new ViviendaRepository(context);

            _visitanteService = new VisitanteService(visitanteRepository);
            _viviendaService = new ViviendaService(viviendaRepository);
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
        }

        // GET: Visitante
        // Listado / historial de visitantes. filtro = "activos" muestra únicamente
        // quienes se encuentran actualmente dentro del condominio.
        public ActionResult Index(string filtro)
        {
            var visitantesDto = filtro == "activos"
                ? _visitanteService.ObtenerActivos()
                : _visitanteService.ObtenerTodos();

            var visitantes = AutoMapperConfig.Mapper.Map<IEnumerable<VisitanteListViewModel>>(visitantesDto);

            ViewBag.Filtro = filtro;

            return View(visitantes);
        }

        // GET: Visitante/Details/5
        public ActionResult Details(int id)
        {
            var visitanteDto = _visitanteService.ObtenerPorId(id);

            if (visitanteDto == null)
                return HttpNotFound();

            var model = AutoMapperConfig.Mapper.Map<VisitanteDetailsViewModel>(visitanteDto);

            return View(model);
        }

        // GET: Visitante/Create
        // Formulario para registrar el ingreso de un visitante o proveedor.
        public ActionResult Create()
        {
            CargarCombos();

            return View(new VisitanteIngresoViewModel { FechaIngreso = DateTime.Now });
        }

        // POST: Visitante/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VisitanteIngresoViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_visitanteService.ExisteVisitanteActivoConIdentificacion(model.Identificacion))
                {
                    ModelState.AddModelError(
                        nameof(model.Identificacion),
                        "Ya existe un visitante con esta identificación dentro del condominio. Debe registrar su salida antes de un nuevo ingreso.");
                }
                else
                {
                    var dto = AutoMapperConfig.Mapper.Map<VisitanteDto>(model);

                    try
                    {
                        _visitanteService.RegistrarIngreso(dto);

                        return RedirectToAction("Index");
                    }
                    catch (InvalidOperationException ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
            }

            CargarCombos();

            return View(model);
        }

        // GET: Visitante/Edit/5
        public ActionResult Edit(int id)
        {
            var visitanteDto = _visitanteService.ObtenerPorId(id);

            if (visitanteDto == null)
                return HttpNotFound();

            var model = AutoMapperConfig.Mapper.Map<VisitanteEditViewModel>(visitanteDto);

            CargarCombos();

            return View(model);
        }

        // POST: Visitante/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VisitanteEditViewModel model)
        {
            if (model.FechaSalida.HasValue && model.FechaSalida.Value < model.FechaIngreso)
            {
                ModelState.AddModelError(
                    nameof(model.FechaSalida),
                    "La fecha de salida no puede ser anterior a la fecha de ingreso.");
            }

            if (ModelState.IsValid)
            {
                var dto = AutoMapperConfig.Mapper.Map<VisitanteDto>(model);

                try
                {
                    _visitanteService.Actualizar(dto);

                    return RedirectToAction("Index");
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            CargarCombos();

            return View(model);
        }

        // GET: Visitante/RegistrarSalida/5
        // Pantalla de confirmación para registrar la salida de un visitante activo.
        public ActionResult RegistrarSalida(int id)
        {
            var visitanteDto = _visitanteService.ObtenerPorId(id);

            if (visitanteDto == null)
                return HttpNotFound();

            if (visitanteDto.FechaSalida != null)
            {
                TempData["Error"] = "Este visitante ya tiene registrada su salida.";
                return RedirectToAction("Index");
            }

            var model = AutoMapperConfig.Mapper.Map<VisitanteDetailsViewModel>(visitanteDto);

            return View(model);
        }

        // POST: Visitante/RegistrarSalida/5
        [HttpPost, ActionName("RegistrarSalida")]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarSalidaConfirmada(int id)
        {
            try
            {
                _visitanteService.RegistrarSalida(id);
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        // GET: Visitante/Delete/5
        public ActionResult Delete(int id)
        {
            var visitanteDto = _visitanteService.ObtenerPorId(id);

            if (visitanteDto == null)
                return HttpNotFound();

            var model = AutoMapperConfig.Mapper.Map<VisitanteDetailsViewModel>(visitanteDto);

            return View(model);
        }

        // POST: Visitante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var visitante = _visitanteService.ObtenerPorId(id);

            if (visitante == null)
                return HttpNotFound();

            _visitanteService.Eliminar(id);

            return RedirectToAction("Index");
        }
    }
}
