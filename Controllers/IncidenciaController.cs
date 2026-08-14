using Condominio.Application.Incidencias;
using Condominio.Application.Mappings;
using Condominio.Domain.Incidencias;
using Condominio.Domain.Residentes;
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
    public class IncidenciaController : Controller
    {
        private readonly IncidenciaService _incidenciaService;
        private readonly ResidenteRepository _residenteRepository;

        public IncidenciaController()
        {
            var context = new CondominioContext();

            var incidenciaRepository = new IncidenciaRepository(context);
            var residenteRepository = new ResidenteRepository(context);

            _incidenciaService = new IncidenciaService(incidenciaRepository);
            _residenteRepository = residenteRepository;
        }

        private bool EsResidente => User.IsInRole("Residente");

        private Residente ObtenerResidenteActual()
        {
            var userId = User.Identity.GetUserId<int>();

            return _residenteRepository.ObtenerPorIdUsuario(userId);
        }

        // GET: Incidencia
        public ActionResult Index()
        {
            IEnumerable<IncidenciaDto> incidenciasDto;

            if (EsResidente)
            {
                var residente = ObtenerResidenteActual();

                incidenciasDto = residente == null
                    ? Enumerable.Empty<IncidenciaDto>()
                    : _incidenciaService.ObtenerPorResidente(residente.IdResidente);
            }
            else
            {
                incidenciasDto = _incidenciaService.ObtenerTodas();
            }

            var incidencias = AutoMapperConfig.Mapper.Map<IEnumerable<IncidenciaListViewModel>>(incidenciasDto);

            ViewBag.EsResidente = EsResidente;

            return View(incidencias);
        }

        // GET: Incidencia/Details/5
        public ActionResult Details(int id)
        {
            var incidenciaDto = _incidenciaService.ObtenerPorId(id);

            if (incidenciaDto == null)
                return HttpNotFound();

            if (EsResidente)
            {
                var residente = ObtenerResidenteActual();

                if (residente == null || incidenciaDto.IdResidente != residente.IdResidente)
                {
                    TempData["Error"] = "No tiene permisos para ver esta incidencia.";
                    return RedirectToAction("Index");
                }
            }

            var model = AutoMapperConfig.Mapper.Map<IncidenciaDetailsViewModel>(incidenciaDto);

            ViewBag.EsResidente = EsResidente;

            return View(model);
        }

        // GET: Incidencia/Create
        public ActionResult Create()
        {
            if (!EsResidente)
            {
                TempData["Error"] = "Solo los residentes pueden reportar incidencias.";
                return RedirectToAction("Index");
            }

            var residente = ObtenerResidenteActual();

            if (residente == null)
            {
                TempData["Error"] = "No se encontró el perfil de residente asociado a su cuenta.";
                return RedirectToAction("Index");
            }

            return View(new IncidenciaCreateViewModel { IdResidente = residente.IdResidente });
        }

        // POST: Incidencia/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IncidenciaCreateViewModel model)
        {
            if (!EsResidente)
            {
                TempData["Error"] = "Solo los residentes pueden reportar incidencias.";
                return RedirectToAction("Index");
            }

            var residente = ObtenerResidenteActual();

            if (residente == null)
            {
                ModelState.AddModelError("", "No se encontró el perfil de residente asociado a su cuenta.");
            }

            if (ModelState.IsValid)
            {
                model.IdResidente = residente.IdResidente;

                var dto = AutoMapperConfig.Mapper.Map<IncidenciaDto>(model);

                dto.FechaReporte = DateTime.Now;
                dto.Estado = EstadoIncidencia.Abierta;

                _incidenciaService.Agregar(dto);

                TempData["Success"] = "Incidencia reportada exitosamente.";

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Incidencia/Edit/5
        public ActionResult Edit(int id)
        {
            if (EsResidente)
            {
                TempData["Error"] = "No tiene permisos para editar incidencias.";
                return RedirectToAction("Index");
            }

            var incidenciaDto = _incidenciaService.ObtenerPorId(id);

            if (incidenciaDto == null)
                return HttpNotFound();

            var model = AutoMapperConfig.Mapper.Map<IncidenciaEditViewModel>(incidenciaDto);

            return View(model);
        }

        // POST: Incidencia/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IncidenciaEditViewModel model)
        {
            if (EsResidente)
            {
                TempData["Error"] = "No tiene permisos para editar incidencias.";
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var incidenciaDto = _incidenciaService.ObtenerPorId(model.IdIncidencia);

                if (incidenciaDto == null)
                    return HttpNotFound();

                incidenciaDto.Estado = model.Estado;
                incidenciaDto.Prioridad = model.Prioridad;

                _incidenciaService.Actualizar(incidenciaDto);

                TempData["Success"] = "Incidencia actualizada exitosamente.";

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Incidencia/Delete/5
        public ActionResult Delete(int id)
        {
            if (!User.IsInRole("Administrador"))
            {
                TempData["Error"] = "No tiene permisos para eliminar incidencias.";
                return RedirectToAction("Index");
            }

            var incidenciaDto = _incidenciaService.ObtenerPorId(id);

            if (incidenciaDto == null)
                return HttpNotFound();

            var model = AutoMapperConfig.Mapper.Map<IncidenciaDetailsViewModel>(incidenciaDto);

            return View(model);
        }

        // POST: Incidencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!User.IsInRole("Administrador"))
            {
                TempData["Error"] = "No tiene permisos para eliminar incidencias.";
                return RedirectToAction("Index");
            }

            var incidenciaDto = _incidenciaService.ObtenerPorId(id);

            if (incidenciaDto == null)
                return HttpNotFound();

            _incidenciaService.Eliminar(id);

            TempData["Success"] = "Incidencia eliminada exitosamente.";

            return RedirectToAction("Index");
        }
    }
}
