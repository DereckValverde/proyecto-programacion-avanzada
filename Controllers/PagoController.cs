using Condominio.Application.DTOs;
using Condominio.Application.Mappings;
using Condominio.Application.Services.Implementations;
using Condominio.Application.ViewModels.Pago;
using Condominio.Domain.Enums;
using Condominio.Infrastructure.DbContexts;
using Condominio.Infrastructure.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Condominio.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class PagoController : Controller
    {
        private readonly PagoService _pagoService;
        private readonly ViviendaService _viviendaService;

        public PagoController()
        {
            var context = new CondominioContext();

            var pagoRepository = new PagoRepository(context);
            var viviendaRepository = new ViviendaRepository(context);

            _pagoService = new PagoService(pagoRepository);
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

        // GET: Pago
        public ActionResult Index(string filtro)
        {
            IEnumerable<PagoDto> pagosDto;

            switch (filtro)
            {
                case "pendientes":
                    pagosDto = _pagoService.ObtenerPorEstado(EstadoPago.Pendiente);
                    break;
                case "pagados":
                    pagosDto = _pagoService.ObtenerPorEstado(EstadoPago.Pagado);
                    break;
                case "atrasados":
                    pagosDto = _pagoService.ObtenerPorEstado(EstadoPago.Atrasado);
                    break;
                default:
                    pagosDto = _pagoService.ObtenerTodos();
                    break;
            }

            var pagos = AutoMapperConfig.Mapper.Map<IEnumerable<PagoListViewModel>>(pagosDto);

            ViewBag.Filtro = filtro;

            return View(pagos);
        }

        // GET: Pago/Details/5
        public ActionResult Details(int id)
        {
            var pagoDto = _pagoService.ObtenerPorId(id);

            if (pagoDto == null)
                return HttpNotFound();

            var model = AutoMapperConfig.Mapper.Map<PagoDetailsViewModel>(pagoDto);

            return View(model);
        }

        // GET: Pago/Create
        public ActionResult Create()
        {
            CargarCombos();

            return View(new PagoCreateViewModel { FechaPago = DateTime.Now });
        }

        // POST: Pago/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PagoCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = AutoMapperConfig.Mapper.Map<PagoDto>(model);

                try
                {
                    _pagoService.Agregar(dto);

                    TempData["Success"] = "Pago registrado exitosamente.";

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

        // GET: Pago/Edit/5
        public ActionResult Edit(int id)
        {
            var pagoDto = _pagoService.ObtenerPorId(id);

            if (pagoDto == null)
                return HttpNotFound();

            var model = AutoMapperConfig.Mapper.Map<PagoEditViewModel>(pagoDto);

            CargarCombos();

            return View(model);
        }

        // POST: Pago/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PagoEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = AutoMapperConfig.Mapper.Map<PagoDto>(model);

                try
                {
                    _pagoService.Actualizar(dto);

                    TempData["Success"] = "Pago actualizado exitosamente.";

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

        // GET: Pago/Delete/5
        public ActionResult Delete(int id)
        {
            var pagoDto = _pagoService.ObtenerPorId(id);

            if (pagoDto == null)
                return HttpNotFound();

            var model = AutoMapperConfig.Mapper.Map<PagoDetailsViewModel>(pagoDto);

            return View(model);
        }

        // POST: Pago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var pago = _pagoService.ObtenerPorId(id);

            if (pago == null)
                return HttpNotFound();

            _pagoService.Eliminar(id);

            TempData["Success"] = "Pago eliminado exitosamente.";

            return RedirectToAction("Index");
        }
    }
}
