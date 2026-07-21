using proyecto_programacion_avanzada.DTOs;
using proyecto_programacion_avanzada.Infrastructure.DbContexts;
using proyecto_programacion_avanzada.Infrastructure.Repositories.Implementations;
using proyecto_programacion_avanzada.Infrastructure.Repositories.Interfaces;
using proyecto_programacion_avanzada.Mappings;
using proyecto_programacion_avanzada.Services.Implementations;
using proyecto_programacion_avanzada.Services.Interfaces;
using proyecto_programacion_avanzada.ViewModels;
using proyecto_programacion_avanzada.ViewModels.Usuario;
using proyecto_programacion_avanzada.ViewModels.Vivienda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyecto_programacion_avanzada.Controllers
{
    public class ViviendaController : Controller
    {

        private readonly ViviendaService _viviendaService;
        private readonly IResidenteRepository _residenteRepository;

        public ViviendaController()
        {
            var context = new CondominioContext();

            var viviendaRepository = new ViviendaRepository(context);
            var residenteRepository = new ResidenteRepository(context);

            _viviendaService = new ViviendaService(viviendaRepository);
            _residenteRepository = residenteRepository;
        }

        public ActionResult Index()
        {
            var viviendasDto = _viviendaService.obtenerTodos();

            var viviendas = AutoMapperConfig.Mapper.Map<IEnumerable<ViviendaListViewModel>>(viviendasDto);

            return View(viviendas);


        }

        //Get: Vivienda/Details/n
        public ActionResult Details(int id)
        {
            var viviendaDto = _viviendaService.ObtenerPorId(id);

            var vivienda = AutoMapperConfig.Mapper.Map<ViviendaDetailsViewModel>(viviendaDto);

            return View(vivienda);
        }

        //Get: Vivienda/Create
        public ActionResult Create()
        {
            return View();
        }

        //Post Vivienda/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViviendaCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = AutoMapperConfig.Mapper.Map<ViviendaDto>(model);

                _viviendaService.Agregar(dto);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        //Get: Vivienda/Edit/n
        public ActionResult Edit(int id)
        {
            var viviendaDto = _viviendaService.ObtenerPorId(id);

            if(viviendaDto == null)
            {
                return HttpNotFound();
            }

            var model = AutoMapperConfig.Mapper.Map<ViviendaEditViewModel>(viviendaDto);

            return View(model);

        }

        //Post: Vivienda/Edit/n
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViviendaEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = AutoMapperConfig.Mapper.Map<ViviendaDto>(model);

                _viviendaService.Actualizar(dto);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        //Get: Vivienda/Delete/n
        public ActionResult Delete(int id)
        {
            var viviendaDto = _viviendaService.ObtenerPorId(id);

            if(viviendaDto == null)
            {
                return HttpNotFound();
            }

            var model = AutoMapperConfig.Mapper.Map<ViviendaDetailsViewModel>(viviendaDto);

            return View(model);
        }

        //Post: Vivienda/Delete/n
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var tieneResidentes = _residenteRepository
                .ObtenerTodos()
                .Any(r => r.IdVivienda == id);


            if (tieneResidentes)
            {
                TempData["Error"] = "No se puede eliminar la vivienda porque tiene residentes asociados.";
                return RedirectToAction("Index");
            }


            _viviendaService.Eliminar(id);

            return RedirectToAction("Index");
        }
    }
}