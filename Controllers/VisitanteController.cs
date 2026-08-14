using AutoMapper;
using Condominio.Application.Mappings;
using Condominio.Application.Visitantes;
using Condominio.Application.Viviendas;
using Condominio.Domain.Residentes;
using Condominio.Domain.Visitantes;
using Condominio.Domain.Viviendas;
using Condominio.Infrastructure.DbContexts;
using Condominio.Infrastructure.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Condominio.Web.Controllers
{
    [Authorize(Roles = "Administrador, Residente, Guarda")]
    public class VisitanteController : Controller
    {
        private readonly VisitanteService _visitanteService;
        private readonly ViviendaService _viviendaService;
        private readonly ResidenteRepository _residenteRepository;

        public VisitanteController()
        {
            var context = new CondominioContext();

            var visitanteRepository = new VisitanteRepository(context);
            var viviendaRepository = new ViviendaRepository(context);
            var residenteRepository = new ResidenteRepository(context);

            _visitanteService = new VisitanteService(visitanteRepository);
            _viviendaService = new ViviendaService(viviendaRepository);
            _residenteRepository = residenteRepository;
        }

        private bool EsResidente => User.IsInRole("Residente");

        private int ObtenerIdViviendaResidente()
        {
            var userId = User.Identity.GetUserId<int>();

            var residente = _residenteRepository.ObtenerPorIdUsuario(userId);

            return residente?.IdVivienda ?? 0;
        }

        private void CargarCombos(int? idViviendaResidente = null)
        {
            IEnumerable<ViviendaDto> viviendas = _viviendaService.obtenerTodos();

            if (idViviendaResidente.HasValue)
            {
                viviendas = viviendas.Where(v => v.IdVivienda == idViviendaResidente.Value).ToList();
            }

            ViewBag.Viviendas = viviendas
                .Select(v => new SelectListItem
                {
                    Value = v.IdVivienda.ToString(),
                    Text = "Bloque " + v.Bloque + " - Vivienda " + v.Numero
                })
                .ToList();

            if (idViviendaResidente.HasValue)
            {
                var vivienda = viviendas.FirstOrDefault();

                ViewBag.ViviendaResidente = vivienda == null
                    ? null
                    : "Bloque " + vivienda.Bloque + " - Vivienda " + vivienda.Numero;
            }
        }

        // GET: Visitante
        public ActionResult Index(string filtro)
        {
            IEnumerable<VisitanteDto> visitantesDto;

            if (EsResidente)
            {
                var idVivienda = ObtenerIdViviendaResidente();

                visitantesDto = _visitanteService.ObtenerHistorialPorVivienda(idVivienda);

                if (filtro == "activos")
                {
                    visitantesDto = visitantesDto.Where(v => v.FechaSalida == null);
                }
            }
            else
            {
                visitantesDto = filtro == "activos"
                    ? _visitanteService.ObtenerActivos()
                    : _visitanteService.ObtenerTodos();
            }

            var visitantes = AutoMapperConfig.Mapper.Map<IEnumerable<VisitanteListViewModel>>(visitantesDto);

            ViewBag.Filtro = filtro;
            ViewBag.EsResidente = EsResidente;

            return View(visitantes);
        }

        // GET: Visitante/Details/5
        public ActionResult Details(int id)
        {
            var visitanteDto = _visitanteService.ObtenerPorId(id);

            if (visitanteDto == null)
                return HttpNotFound();

            if (EsResidente && visitanteDto.IdVivienda != ObtenerIdViviendaResidente())
            {
                TempData["Error"] = "No tiene permisos para ver este visitante.";
                return RedirectToAction("Index");
            }

            var model = AutoMapperConfig.Mapper.Map<VisitanteDetailsViewModel>(visitanteDto);

            return View(model);
        }

        // GET: Visitante/Create
        public ActionResult Create()
        {
            var idViviendaResidente = EsResidente ? ObtenerIdViviendaResidente() : (int?)null;

            ViewBag.EsResidente = EsResidente;

            CargarCombos(idViviendaResidente);

            return View(new VisitanteIngresoViewModel
            {
                FechaIngreso = DateTime.Now,
                IdVivienda = idViviendaResidente ?? 0
            });
        }

        // POST: Visitante/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VisitanteIngresoViewModel model)
        {
            if (EsResidente)
            {
                model.IdVivienda = ObtenerIdViviendaResidente();
            }

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

                        TempData["Success"] = "Ingreso registrado exitosamente.";

                        return RedirectToAction("Index");
                    }
                    catch (InvalidOperationException ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
            }

            ViewBag.EsResidente = EsResidente;

            CargarCombos(EsResidente ? model.IdVivienda : (int?)null);

            return View(model);
        }

        // GET: Visitante/Edit/5
        public ActionResult Edit(int id)
        {
            var visitanteDto = _visitanteService.ObtenerPorId(id);

            if (visitanteDto == null)
                return HttpNotFound();

            if (EsResidente && visitanteDto.IdVivienda != ObtenerIdViviendaResidente())
            {
                TempData["Error"] = "No tiene permisos para editar este visitante.";
                return RedirectToAction("Index");
            }

            var model = AutoMapperConfig.Mapper.Map<VisitanteEditViewModel>(visitanteDto);

            ViewBag.EsResidente = EsResidente;

            CargarCombos(EsResidente ? visitanteDto.IdVivienda : (int?)null);

            return View(model);
        }

        // POST: Visitante/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VisitanteEditViewModel model)
        {
            if (EsResidente)
            {
                var idViviendaResidente = ObtenerIdViviendaResidente();

                if (model.IdVivienda != idViviendaResidente)
                {
                    TempData["Error"] = "No tiene permisos para editar este visitante.";
                    return RedirectToAction("Index");
                }

                model.IdVivienda = idViviendaResidente;
            }

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

                    TempData["Success"] = "Visitante actualizado exitosamente.";

                    return RedirectToAction("Index");
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            ViewBag.EsResidente = EsResidente;

            CargarCombos(EsResidente ? model.IdVivienda : (int?)null);

            return View(model);
        }

        // GET: Visitante/RegistrarSalida/5
        public ActionResult RegistrarSalida(int id)
        {
            var visitanteDto = _visitanteService.ObtenerPorId(id);

            if (visitanteDto == null)
                return HttpNotFound();

            if (EsResidente && visitanteDto.IdVivienda != ObtenerIdViviendaResidente())
            {
                TempData["Error"] = "No tiene permisos para registrar la salida de este visitante.";
                return RedirectToAction("Index");
            }

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
            var visitanteDto = _visitanteService.ObtenerPorId(id);

            if (visitanteDto != null &&
                EsResidente &&
                visitanteDto.IdVivienda != ObtenerIdViviendaResidente())
            {
                TempData["Error"] = "No tiene permisos para registrar la salida de este visitante.";
                return RedirectToAction("Index");
            }

            try
            {
                _visitanteService.RegistrarSalida(id);

                TempData["Success"] = "Salida registrada exitosamente.";
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

            if (EsResidente && visitanteDto.IdVivienda != ObtenerIdViviendaResidente())
            {
                TempData["Error"] = "No tiene permisos para eliminar este visitante.";
                return RedirectToAction("Index");
            }

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

            if (EsResidente && visitante.IdVivienda != ObtenerIdViviendaResidente())
            {
                TempData["Error"] = "No tiene permisos para eliminar este visitante.";
                return RedirectToAction("Index");
            }

            _visitanteService.Eliminar(id);

            TempData["Success"] = "Visitante eliminado exitosamente.";

            return RedirectToAction("Index");
        }
    }
}
