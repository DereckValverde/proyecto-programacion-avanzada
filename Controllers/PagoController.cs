using Condominio.Application.DTOs;
using Condominio.Application.Mappings;
using Condominio.Application.Services.Implementations;
using Condominio.Application.ViewModels.Pago;
using Condominio.Domain.Enums;
using Condominio.Infrastructure.DbContexts;
using Condominio.Infrastructure.Repositories.Implementations;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Condominio.Web.Controllers
{
    [Authorize(Roles = "Administrador, Residente")]
    public class PagoController : Controller
    {
        private readonly PagoService _pagoService;
        private readonly ViviendaService _viviendaService;
        private readonly ResidenteRepository _residenteRepository;

        public PagoController()
        {
            var context = new CondominioContext();

            var pagoRepository = new PagoRepository(context);
            var viviendaRepository = new ViviendaRepository(context);
            var residenteRepository = new ResidenteRepository(context);

            _pagoService = new PagoService(pagoRepository);
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

        // GET: Pago
        public ActionResult Index(string filtro)
        {
            IEnumerable<PagoDto> pagosDto;

            if (EsResidente)
            {
                pagosDto = _pagoService.ObtenerPorVivienda(ObtenerIdViviendaResidente());

                switch (filtro)
                {
                    case "pendientes":
                        pagosDto = pagosDto.Where(p => p.Estado == EstadoPago.Pendiente);
                        break;
                    case "pagados":
                        pagosDto = pagosDto.Where(p => p.Estado == EstadoPago.Pagado);
                        break;
                    case "atrasados":
                        pagosDto = pagosDto.Where(p => p.Estado == EstadoPago.Atrasado);
                        break;
                }
            }
            else
            {
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
            }

            var pagos = AutoMapperConfig.Mapper.Map<IEnumerable<PagoListViewModel>>(pagosDto);

            ViewBag.Filtro = filtro;
            ViewBag.EsResidente = EsResidente;

            return View(pagos);
        }

        // GET: Pago/Details/5
        public ActionResult Details(int id)
        {
            var pagoDto = _pagoService.ObtenerPorId(id);

            if (pagoDto == null)
                return HttpNotFound();

            if (EsResidente && pagoDto.IdVivienda != ObtenerIdViviendaResidente())
            {
                TempData["Error"] = "No tiene permisos para ver este pago.";
                return RedirectToAction("Index");
            }

            var model = AutoMapperConfig.Mapper.Map<PagoDetailsViewModel>(pagoDto);

            return View(model);
        }

        // GET: Pago/Create
        public ActionResult Create()
        {
            var idViviendaResidente = EsResidente ? ObtenerIdViviendaResidente() : (int?)null;

            ViewBag.EsResidente = EsResidente;

            CargarCombos(idViviendaResidente);

            return View(new PagoCreateViewModel
            {
                FechaPago = DateTime.Now,
                IdVivienda = idViviendaResidente ?? 0
            });
        }

        // POST: Pago/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PagoCreateViewModel model)
        {
            if (EsResidente)
            {
                model.IdVivienda = ObtenerIdViviendaResidente();
                model.Estado = EstadoPago.Pendiente;
            }

            if (ModelState.IsValid)
            {
                var dto = AutoMapperConfig.Mapper.Map<PagoDto>(model);

                try
                {
                    _pagoService.Agregar(dto);

                    TempData["Success"] = "Pago registrado exitosamente. Queda pendiente de confirmación por el administrador.";

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

        // GET: Pago/Edit/5
        public ActionResult Edit(int id)
        {
            if (EsResidente)
            {
                TempData["Error"] = "No tiene permisos para editar pagos.";
                return RedirectToAction("Index");
            }

            var pagoDto = _pagoService.ObtenerPorId(id);

            if (pagoDto == null)
                return HttpNotFound();

            var model = AutoMapperConfig.Mapper.Map<PagoEditViewModel>(pagoDto);

            ViewBag.EsResidente = EsResidente;

            CargarCombos(EsResidente ? pagoDto.IdVivienda : (int?)null);

            return View(model);
        }

        // POST: Pago/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PagoEditViewModel model)
        {
            if (EsResidente)
            {
                TempData["Error"] = "No tiene permisos para editar pagos.";
                return RedirectToAction("Index");
            }

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

            ViewBag.EsResidente = EsResidente;

            CargarCombos(EsResidente ? model.IdVivienda : (int?)null);

            return View(model);
        }

        // GET: Pago/Delete/5
        public ActionResult Delete(int id)
        {
            if (EsResidente)
            {
                TempData["Error"] = "No tiene permisos para eliminar pagos.";
                return RedirectToAction("Index");
            }

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
            if (EsResidente)
            {
                TempData["Error"] = "No tiene permisos para eliminar pagos.";
                return RedirectToAction("Index");
            }

            var pago = _pagoService.ObtenerPorId(id);

            if (pago == null)
                return HttpNotFound();

            _pagoService.Eliminar(id);

            TempData["Success"] = "Pago eliminado exitosamente.";

            return RedirectToAction("Index");
        }
    }
}
