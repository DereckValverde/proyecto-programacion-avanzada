using Condominio.Application.Mappings;
using Condominio.Application.Usuarios;
using Condominio.Domain.Residentes;
using Condominio.Domain.Usuarios;
using Condominio.Domain.Viviendas;
using Condominio.Infrastructure.DbContexts;
using Condominio.Infrastructure.Repositories;
using Condominio.Identity;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Condominio.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsuarioController : Controller
    {

        private readonly UsuarioService _usuarioService;
        private readonly ResidenteRepository _residenteRepository;
        private readonly ViviendaRepository _viviendaRepository;

        public UsuarioController()
        {
            var context = new CondominioContext();

            var usuarioRepository = new UsuarioRepository(context);

            _residenteRepository = new ResidenteRepository(context);
            _viviendaRepository = new ViviendaRepository(context);
            _usuarioService = new UsuarioService(usuarioRepository);
        }

        private void CargarViviendas()
        {
            ViewBag.Viviendas = _viviendaRepository
                .ObtenerTodos()
                .Select(v => new SelectListItem
                {
                    Value = v.IdVivienda.ToString(),
                    Text = "Bloque " + v.Bloque + " - Vivienda " + v.Numero
                })
                .ToList();
        }

        private IEnumerable<SelectListItem> ObtenerViviendas(int? idViviendaSeleccionada = null)
        {
            return _viviendaRepository
                .ObtenerTodos()
                .Select(v => new SelectListItem
                {
                    Value = v.IdVivienda.ToString(),
                    Text = v.Numero,
                    Selected = v.IdVivienda == idViviendaSeleccionada
                })
                .ToList();
        }

        public ActionResult Index()
        {
            var usuariosDto = _usuarioService.ObtenerTodos();

            var usuario = AutoMapperConfig.Mapper.Map<IEnumerable<UsuarioListViewModel>>(usuariosDto);


            return View(usuario);
        }

        //Get: Usuario/Details/n
        public ActionResult Details(int id)
        {
            var usuarioDto = _usuarioService.ObtenerPorId(id);

            if(usuarioDto == null)
            {
                return HttpNotFound();
            }

            var usuario = AutoMapperConfig.Mapper.Map<UsuarioDetailsViewModel>(usuarioDto);

            return View(usuario);
        }

        //Get: Usuario/Create
        public ActionResult Create()
        {
            CargarViviendas();

            return View(new UsuarioCreateViewModel());
        }

        //POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                CargarViviendas();
                return View(model);
            }

            var usuario = new Usuario
            {
                UserName = model.Correo,
                Nombre = model.Nombre,
                Telefono = model.Telefono,
                Rol = model.Rol,
                Estado = model.Estado
            };

            using (var context = new CondominioContext())
            using (var userManager = ApplicationUserManager.Create(context))
            {
                var resultado = userManager.Create(usuario, model.Contrasena);

                if (!resultado.Succeeded)
                {
                    foreach (var error in resultado.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }

                    CargarViviendas();
                    return View(model);
                }

                ApplicationUserManager.AsegurarRol(context, model.Rol.ToString());

                if (!userManager.IsInRole(usuario.Id, model.Rol.ToString()))
                {
                    userManager.AddToRole(usuario.Id, model.Rol.ToString());
                }
            }

            if (model.Rol == RolUsuario.Residente)
            {
                var residente = new Residente
                {
                    Nombre = model.Nombre,
                    FechaIngreso = model.FechaIngreso.Value,
                    Estado = model.Estado,
                    IdUsuario = usuario.Id,
                    IdVivienda = model.IdVivienda.Value
                };

                _residenteRepository.Agregar(residente);
                _residenteRepository.Guardar();
            }

            TempData["Success"] = "Usuario registrado exitosamente.";

            return RedirectToAction("Index");
        }

        //Get: Usuario/Edit/n
        public ActionResult Edit(int id)
        {
            var usuario = _usuarioService.ObtenerPorId(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }


            var model = new UsuarioEditViewModel
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Telefono = usuario.Telefono,
                Rol = usuario.Rol,
                Estado = usuario.Estado
            };


            if (usuario.Rol == RolUsuario.Residente)
            {
                var residente = _residenteRepository.ObtenerPorIdUsuario(id);

                if (residente != null)
                {
                    model.FechaIngreso = residente.FechaIngreso;
                    model.IdVivienda = residente.IdVivienda;
                }
            }


            model.Viviendas = ObtenerViviendas(model.IdVivienda);


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsuarioEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Viviendas = ObtenerViviendas(model.IdVivienda);
                return View(model);
            }

            var usuarioDto = new UsuarioDto
            {
                IdUsuario = model.IdUsuario,
                Nombre = model.Nombre,
                Correo = model.Correo,
                Telefono = model.Telefono,
                Rol = model.Rol,
                Estado = model.Estado
            };


            _usuarioService.Actualizar(usuarioDto);

            using (var context = new CondominioContext())
            using (var userManager = ApplicationUserManager.Create(context))
            {
                var usuario = userManager.FindById(model.IdUsuario);

                if (usuario != null)
                {
                    foreach (var rol in userManager.GetRoles(usuario.Id))
                    {
                        userManager.RemoveFromRole(usuario.Id, rol);
                    }

                    userManager.AddToRole(usuario.Id, model.Rol.ToString());
                }
            }


            if (model.Rol == RolUsuario.Residente)
            {
                var residente = _residenteRepository.ObtenerPorIdUsuario(model.IdUsuario);

                if (residente != null)
                {
                    residente.Nombre = model.Nombre;
                    residente.FechaIngreso = model.FechaIngreso.Value;
                    residente.Estado = model.Estado;
                    residente.IdVivienda = model.IdVivienda.Value;

                    _residenteRepository.Actualizar(residente);
                    _residenteRepository.Guardar();
                }
            }

            TempData["Success"] = "Usuario actualizado exitosamente.";

            return RedirectToAction("Index");
        }

        //Get: Usuario/Delete/n
        public ActionResult Delete(int id)
        {
            var usuarioDto = _usuarioService.ObtenerPorId(id);

            if (usuarioDto == null)
            {
                return HttpNotFound();
            }

            var model = AutoMapperConfig.Mapper.Map<UsuarioDetailsViewModel>(usuarioDto);

            return View(model);
        }

        //Post: Usuario/Delete/n
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var residente = _residenteRepository.ObtenerPorIdUsuario(id);

            if(residente != null)
            {
                _residenteRepository.Eliminar(residente.IdResidente);
                _residenteRepository.Guardar();
            }

            _usuarioService.Eliminar(id);

            TempData["Success"] = "Usuario eliminado exitosamente.";

            return RedirectToAction("Index");
        }
    }
}
