using proyecto_programacion_avanzada.DTOs;
using proyecto_programacion_avanzada.Infrastructure.DbContexts;
using proyecto_programacion_avanzada.Infrastructure.Repositories.Implementations;
using proyecto_programacion_avanzada.Mappings;
using proyecto_programacion_avanzada.Services.Implementations;
using proyecto_programacion_avanzada.ViewModels.AreaComun;
using System.Collections.Generic;
using System.Web.Mvc;

namespace proyecto_programacion_avanzada.Controllers
{
    public class AreaComunController : Controller
    {
        private readonly AreaComunService _areaComunService;

        public AreaComunController()
        {
            var context = new CondominioContext();

            var areaComunRepository = new AreaComunRepository(context);

            _areaComunService = new AreaComunService(areaComunRepository);
        }

        // GET: AreaComun
        public ActionResult Index()
        {
            var areasComunesDto = _areaComunService.ObtenerTodos();

            var areasComunes =
                AutoMapperConfig.Mapper.Map<IEnumerable<AreaComunListViewModel>>(areasComunesDto);

            return View(areasComunes);
        }

        // GET: AreaComun/Details/5
        public ActionResult Details(int id)
        {
            var areaComunDto = _areaComunService.ObtenerPorId(id);

            if (areaComunDto == null)
            {
                return HttpNotFound();
            }

            var areaComun =
                AutoMapperConfig.Mapper.Map<AreaComunDetailsViewModel>(areaComunDto);

            return View(areaComun);
        }

        // GET: AreaComun/Create
        public ActionResult Create()
        {
            return View(new AreaComunCreateViewModel());
        }

        // POST: AreaComun/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AreaComunCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.HoraCierre <= model.HoraApertura)
            {
                ModelState.AddModelError(
                    "HoraCierre",
                    "La hora de cierre debe ser mayor que la hora de apertura.");

                return View(model);
            }

            var areaComunDto =
                AutoMapperConfig.Mapper.Map<AreaComunDto>(model);

            _areaComunService.Agregar(areaComunDto);

            TempData["Success"] = "El área común fue registrada correctamente.";

            return RedirectToAction("Index");
        }

        // GET: AreaComun/Edit/5
        public ActionResult Edit(int id)
        {
            var areaComunDto = _areaComunService.ObtenerPorId(id);

            if (areaComunDto == null)
            {
                return HttpNotFound();
            }

            var areaComun =
                AutoMapperConfig.Mapper.Map<AreaComunEditViewModel>(areaComunDto);

            return View(areaComun);
        }

        // POST: AreaComun/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AreaComunEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.HoraCierre <= model.HoraApertura)
            {
                ModelState.AddModelError(
                    "HoraCierre",
                    "La hora de cierre debe ser mayor que la hora de apertura.");

                return View(model);
            }

            var areaComunExistente =
                _areaComunService.ObtenerPorId(model.IdArea);

            if (areaComunExistente == null)
            {
                return HttpNotFound();
            }

            var areaComunDto =
                AutoMapperConfig.Mapper.Map<AreaComunDto>(model);

            _areaComunService.Actualizar(areaComunDto);

            TempData["Success"] = "El área común fue actualizada correctamente.";

            return RedirectToAction("Index");
        }

        // GET: AreaComun/Delete/5
        public ActionResult Delete(int id)
        {
            var areaComunDto = _areaComunService.ObtenerPorId(id);

            if (areaComunDto == null)
            {
                return HttpNotFound();
            }

            var areaComun =
                AutoMapperConfig.Mapper.Map<AreaComunDetailsViewModel>(areaComunDto);

            return View(areaComun);
        }

        // POST: AreaComun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var areaComunDto = _areaComunService.ObtenerPorId(id);

            if (areaComunDto == null)
            {
                return HttpNotFound();
            }

            _areaComunService.Eliminar(id);

            TempData["Success"] = "El área común fue eliminada correctamente.";

            return RedirectToAction("Index");
        }
    }
}