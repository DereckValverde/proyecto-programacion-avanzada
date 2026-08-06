using proyecto_programacion_avanzada.DTOs;
using proyecto_programacion_avanzada.Entities;
using proyecto_programacion_avanzada.Infrastructure.Repositories.Interfaces;
using proyecto_programacion_avanzada.Mappings;
using proyecto_programacion_avanzada.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace proyecto_programacion_avanzada.Services.Implementations
{
    public class NoticiaService : INoticiaService
    {
        private readonly INoticiaRepository _noticiaRepository;

        public NoticiaService(INoticiaRepository noticiaRepository)
        {
            _noticiaRepository = noticiaRepository;
        }

        public IEnumerable<NoticiaDto> ObtenerTodas()
        {
            var noticias = _noticiaRepository.ObtenerTodas();

            return noticias.Select(n =>
                AutoMapperConfig.Mapper.Map<NoticiaDto>(n));
        }

        public NoticiaDto ObtenerPorId(int id)
        {
            var noticia = _noticiaRepository.ObtenerPorId(id);

            if (noticia == null)
            {
                return null;
            }

            return AutoMapperConfig.Mapper.Map<NoticiaDto>(noticia);
        }

        public void Agregar(NoticiaDto noticiaDto)
        {
            var noticia = AutoMapperConfig.Mapper.Map<Noticia>(noticiaDto);

            _noticiaRepository.Agregar(noticia);
            _noticiaRepository.Guardar();
        }

        public void Actualizar(NoticiaDto noticiaDto)
        {
            var noticia = AutoMapperConfig.Mapper.Map<Noticia>(noticiaDto);

            _noticiaRepository.Actualizar(noticia);
            _noticiaRepository.Guardar();
        }

        public void Eliminar(int id)
        {
            _noticiaRepository.Eliminar(id);
            _noticiaRepository.Guardar();
        }
    }
}
