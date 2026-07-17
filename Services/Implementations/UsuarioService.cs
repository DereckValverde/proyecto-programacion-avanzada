using proyecto_programacion_avanzada.DTOs;
using proyecto_programacion_avanzada.Entities;
using proyecto_programacion_avanzada.Infrastructure.Repositories.Interfaces;
using proyecto_programacion_avanzada.Mappings;
using proyecto_programacion_avanzada.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyecto_programacion_avanzada.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public void Actualizar(UsuarioDto usuarioDto)
        {
            var usuario = AutoMapperConfig.Mapper.Map<Usuario>(usuarioDto);

            _usuarioRepository.Actualizar(usuario);
            _usuarioRepository.Guardar();
        }

        public void Agregar(UsuarioDto usuarioDto)
        {
            var usuario = AutoMapperConfig.Mapper.Map<Usuario>(usuarioDto);

            _usuarioRepository.Agregar(usuario);
            _usuarioRepository.Guardar();
        }

        public void Eliminar(int id)
        {
            _usuarioRepository.Eliminar(id);
            _usuarioRepository.Guardar();
        }

        public UsuarioDto ObtenerPorId(int id)
        {
            var usuario = _usuarioRepository.ObtenerPorId(id);

            if(usuario == null)
            {
                return null;
            }

            return AutoMapperConfig.Mapper.Map<UsuarioDto>(usuario);
        }

        public IEnumerable<UsuarioDto> ObtenerTodos()
        {
            var usuarios = _usuarioRepository.ObtenerTodos();

            return usuarios.Select(usuario =>
                AutoMapperConfig.Mapper.Map<UsuarioDto>(usuario));
        }
    }
}