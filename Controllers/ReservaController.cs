using Condominio.Application.DTOs;
using Condominio.Infrastructure.DbContexts;
using Condominio.Infrastructure.Repositories.Implementations;
using Condominio.Application.Mappings;
using Condominio.Application.Services.Implementations;
using Condominio.Application.ViewModels.Reserva;
using Condominio.Domain.Enums;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Condominio.Web.Controllers
{
    [Authorize(Roles = "Administrador, Residente")]
    public class ReservaController : Controller
    {
        private readonly ReservaService _reservaService;
        private readonly ViviendaService _viviendaService;
        private readonly AreaComunService _areaComunService;
        private readonly ResidenteRepository _residenteRepository;

        public ReservaController()
        {
            var context = new CondominioContext();

            var reservaRepository = new ReservaRepository(context);
            var viviendaRepository = new ViviendaRepository(context);
            var areaComunRepository = new AreaComunRepository(context);
            var residenteRepository = new ResidenteRepository(context);

            _reservaService = new ReservaService(reservaRepository);
            _viviendaService = new ViviendaService(viviendaRepository);
            _areaComunService = new AreaComunService(areaComunRepository);
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
            IEnumerable<ReservaDto> reservasDto;

            if (EsResidente)
            {
                reservasDto = _reservaService.ObtenerPorVivienda(ObtenerIdViviendaResidente());
            }
            else
            {
                reservasDto = _reservaService.ObtenerTodos();
            }

            var reservas = AutoMapperConfig.Mapper.Map<IEnumerable<ReservaListViewModel>>(reservasDto);

            ViewBag.EsResidente = EsResidente;

            return View(reservas);
        }

        // GET: Reserva/Details/5
        public ActionResult Details(int id)
        {
            var reservaDto = _reservaService.ObtenerPorId(id);

            if (reservaDto == null)
                return HttpNotFound();

            if (EsResidente && reservaDto.IdVivienda != ObtenerIdViviendaResidente())
            {
                TempData["Error"] = "No tiene permisos para ver esta reserva.";
                return RedirectToAction("Index");
            }

            var model = AutoMapperConfig.Mapper.Map<ReservaDetailsViewModel>(reservaDto);

            return View(model);
        }

        // GET: Reserva/Create
        public ActionResult Create()
        {
            var idViviendaResidente = EsResidente ? ObtenerIdViviendaResidente() : (int?)null;

            ViewBag.EsResidente = EsResidente;

            CargarCombos(idViviendaResidente);

            return View(new ReservaCreateViewModel { IdVivienda = idViviendaResidente ?? 0 });
        }

        // POST: Reserva/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReservaCreateViewModel model)
        {
            if (EsResidente)
            {
                model.IdVivienda = ObtenerIdViviendaResidente();
                model.Estado = EstadoReserva.Pendiente;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var dto = AutoMapperConfig.Mapper.Map<ReservaDto>(model);

                    _reservaService.Agregar(dto);

                    TempData["Success"] = "Reserva registrada exitosamente. Queda pendiente de aprobación por el administrador.";

                    return RedirectToAction("Index");
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            CargarCombos(EsResidente ? model.IdVivienda : (int?)null);

            ViewBag.EsResidente = EsResidente;

            return View(model);
        }

        // GET: Reserva/Edit/5
        public ActionResult Edit(int id)
        {
            var reservaDto = _reservaService.ObtenerPorId(id);

            if (reservaDto == null)
                return HttpNotFound();

            if (EsResidente && reservaDto.IdVivienda != ObtenerIdViviendaResidente())
            {
                TempData["Error"] = "No tiene permisos para editar esta reserva.";
                return RedirectToAction("Index");
            }

            if (EsResidente && reservaDto.Estado != EstadoReserva.Pendiente)
            {
                TempData["Error"] = "Solo puede editar reservas en estado Pendiente.";
                return RedirectToAction("Index");
            }

            var model = AutoMapperConfig.Mapper.Map<ReservaEditViewModel>(reservaDto);

            ViewBag.EsResidente = EsResidente;

            CargarCombos(EsResidente ? reservaDto.IdVivienda : (int?)null);

            return View(model);
        }

        // POST: Reserva/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReservaEditViewModel model)
        {
            if (EsResidente)
            {
                var idViviendaResidente = ObtenerIdViviendaResidente();

                var reservaActual = _reservaService.ObtenerPorId(model.IdReserva);

                if (reservaActual == null)
                    return HttpNotFound();

                if (reservaActual.IdVivienda != idViviendaResidente)
                {
                    TempData["Error"] = "No tiene permisos para editar esta reserva.";
                    return RedirectToAction("Index");
                }

                if (reservaActual.Estado != EstadoReserva.Pendiente)
                {
                    TempData["Error"] = "Solo puede editar reservas en estado Pendiente.";
                    return RedirectToAction("Index");
                }

                model.IdVivienda = idViviendaResidente;
                model.Estado = EstadoReserva.Pendiente;
            }

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

            CargarCombos(EsResidente ? model.IdVivienda : (int?)null);

            ViewBag.EsResidente = EsResidente;

            return View(model);
        }

        // GET: Reserva/Delete/5
        public ActionResult Delete(int id)
        {
            if (EsResidente)
            {
                TempData["Error"] = "No tiene permisos para eliminar reservas. Puede cancelar la reserva desde el listado.";
                return RedirectToAction("Index");
            }

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
            if (EsResidente)
            {
                TempData["Error"] = "No tiene permisos para eliminar reservas. Puede cancelar la reserva desde el listado.";
                return RedirectToAction("Index");
            }

            var reservaDto = _reservaService.ObtenerPorId(id);

            if (reservaDto == null)
                return HttpNotFound();

            _reservaService.Eliminar(id);

            TempData["Success"] = "Reserva eliminada exitosamente.";

            return RedirectToAction("Index");
        }

        // GET: Reserva/Cancelar/5
        public ActionResult Cancelar(int id)
        {
            if (!EsResidente)
            {
                TempData["Error"] = "Solo los residentes pueden cancelar reservas.";
                return RedirectToAction("Index");
            }

            var reservaDto = _reservaService.ObtenerPorId(id);

            if (reservaDto == null)
                return HttpNotFound();

            if (reservaDto.IdVivienda != ObtenerIdViviendaResidente())
            {
                TempData["Error"] = "No tiene permisos para cancelar esta reserva.";
                return RedirectToAction("Index");
            }

            if (reservaDto.Estado != EstadoReserva.Pendiente)
            {
                TempData["Error"] = "Solo puede cancelar reservas en estado Pendiente.";
                return RedirectToAction("Index");
            }

            var model = AutoMapperConfig.Mapper.Map<ReservaDetailsViewModel>(reservaDto);

            return View(model);
        }

        // POST: Reserva/Cancelar/5
        [HttpPost, ActionName("Cancelar")]
        [ValidateAntiForgeryToken]
        public ActionResult CancelarConfirmed(int id)
        {
            if (!EsResidente)
            {
                TempData["Error"] = "Solo los residentes pueden cancelar reservas.";
                return RedirectToAction("Index");
            }

            var reservaDto = _reservaService.ObtenerPorId(id);

            if (reservaDto == null)
                return HttpNotFound();

            if (reservaDto.IdVivienda != ObtenerIdViviendaResidente())
            {
                TempData["Error"] = "No tiene permisos para cancelar esta reserva.";
                return RedirectToAction("Index");
            }

            if (reservaDto.Estado != EstadoReserva.Pendiente)
            {
                TempData["Error"] = "Solo puede cancelar reservas en estado Pendiente.";
                return RedirectToAction("Index");
            }

            reservaDto.Estado = EstadoReserva.Cancelada;

            _reservaService.Actualizar(reservaDto);

            TempData["Success"] = "Reserva cancelada exitosamente.";

            return RedirectToAction("Index");
        }
    }
}
