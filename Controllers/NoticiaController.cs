using Condominio.Application.Mappings;
using Condominio.Application.Noticias;
using Condominio.Domain.Noticias;
using Condominio.Infrastructure.DbContexts;
using Condominio.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace Condominio.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class NoticiaController : Controller
    {
        private readonly INoticiaService _noticiaService;

        public NoticiaController()
        {
            var context = new CondominioContext();

            _noticiaService = new NoticiaService(new NoticiaRepository(context));
        }

        private string NombreUsuarioActual()
        {
            var principal = User as ClaimsPrincipal;

            var claim = principal?.FindFirst("Nombre");

            return claim?.Value ?? User.Identity.Name;
        }

        // GET: Noticia
        public ActionResult Index()
        {
            var noticiasDto = _noticiaService.ObtenerTodas();

            var noticias = AutoMapperConfig.Mapper.Map<IEnumerable<NoticiaListViewModel>>(noticiasDto);

            return View(noticias);
        }

        // GET: Noticia/Details/5
        public ActionResult Details(int id)
        {
            var noticiaDto = _noticiaService.ObtenerPorId(id);

            if (noticiaDto == null)
            {
                return HttpNotFound();
            }

            var model = AutoMapperConfig.Mapper.Map<NoticiaDetailsViewModel>(noticiaDto);

            return View(model);
        }

        // GET: Noticia/Create
        public ActionResult Create()
        {
            return View(new NoticiaCreateViewModel
            {
                Autor = NombreUsuarioActual()
            });
        }

        // POST: Noticia/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoticiaCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = AutoMapperConfig.Mapper.Map<NoticiaDto>(model);

                dto.FechaPublicacion = DateTime.Now;
                dto.Autor = string.IsNullOrWhiteSpace(dto.Autor)
                    ? NombreUsuarioActual()
                    : dto.Autor;

                _noticiaService.Agregar(dto);

                TempData["Success"] = "Noticia registrada exitosamente.";

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Noticia/Edit/5
        public ActionResult Edit(int id)
        {
            var noticiaDto = _noticiaService.ObtenerPorId(id);

            if (noticiaDto == null)
            {
                return HttpNotFound();
            }

            var model = AutoMapperConfig.Mapper.Map<NoticiaEditViewModel>(noticiaDto);

            return View(model);
        }

        // POST: Noticia/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NoticiaEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = AutoMapperConfig.Mapper.Map<NoticiaDto>(model);

                var existente = _noticiaService.ObtenerPorId(dto.IdNoticia);

                if (existente == null)
                {
                    return HttpNotFound();
                }

                dto.FechaPublicacion = existente.FechaPublicacion;

                _noticiaService.Actualizar(dto);

                TempData["Success"] = "Noticia actualizada exitosamente.";

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Noticia/Delete/5
        public ActionResult Delete(int id)
        {
            var noticiaDto = _noticiaService.ObtenerPorId(id);

            if (noticiaDto == null)
            {
                return HttpNotFound();
            }

            var model = AutoMapperConfig.Mapper.Map<NoticiaDetailsViewModel>(noticiaDto);

            return View(model);
        }

        // POST: Noticia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _noticiaService.Eliminar(id);

            TempData["Success"] = "Noticia eliminada exitosamente.";

            return RedirectToAction("Index");
        }
    }
}
