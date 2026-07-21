using proyecto_programacion_avanzada.Entities;
using proyecto_programacion_avanzada.Infrastructure.DbContexts;
using proyecto_programacion_avanzada.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyecto_programacion_avanzada.Infrastructure.Repositories.Implementations
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly CondominioContext _context;

        public UsuarioRepository(CondominioContext context)
        {
            _context = context;
        }

        public void Actualizar(Usuario usuario)
        {
            var usuarioExistente = _context.Usuarios.Find(usuario.IdUsuario);

            if(usuarioExistente == null)
            {
                return;
            }

            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.Correo = usuario.Correo;
            usuarioExistente.Telefono = usuario.Telefono;
            usuarioExistente.Rol = usuario.Rol;
            usuarioExistente.Estado = usuario.Estado;
        }

        public void Agregar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
        }

        public void Eliminar(int id)
        {
            Usuario usuario = _context.Usuarios.Find(id);

            if(usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }
        }

        public void Guardar()
        {

            _context.SaveChanges();
        }

        public Usuario ObtenerPorId(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public IEnumerable<Usuario> ObtenerTodos()
        {
            return _context.Usuarios.ToList();
        }
    }
}