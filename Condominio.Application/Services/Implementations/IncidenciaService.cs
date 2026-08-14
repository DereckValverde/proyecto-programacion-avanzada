using Condominio.Application.DTOs;
using Condominio.Domain.Entities;
using Condominio.Application.Repositories.Interfaces;
using Condominio.Application.Mappings;
using Condominio.Application.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Condominio.Application.Services.Implementations
{
    public class IncidenciaService : IIncidenciaService
    {
        private readonly IIncidenciaRepository _incidenciaRepository;

        public IncidenciaService(IIncidenciaRepository incidenciaRepository)
        {
            _incidenciaRepository = incidenciaRepository;
        }

        public IEnumerable<IncidenciaDto> ObtenerTodas()
        {
            var incidencias = _incidenciaRepository.ObtenerTodas();

            return incidencias.Select(i => AutoMapperConfig.Mapper.Map<IncidenciaDto>(i));
        }

        public IEnumerable<IncidenciaDto> ObtenerPorResidente(int idResidente)
        {
            var incidencias = _incidenciaRepository.ObtenerPorResidente(idResidente);

            return incidencias.Select(i => AutoMapperConfig.Mapper.Map<IncidenciaDto>(i));
        }

        public IncidenciaDto ObtenerPorId(int id)
        {
            var incidencia = _incidenciaRepository.ObtenerPorId(id);

            return incidencia == null
                ? null
                : AutoMapperConfig.Mapper.Map<IncidenciaDto>(incidencia);
        }

        public void Agregar(IncidenciaDto incidenciaDto)
        {
            var incidencia = AutoMapperConfig.Mapper.Map<Incidencia>(incidenciaDto);

            _incidenciaRepository.Agregar(incidencia);
            _incidenciaRepository.Guardar();
        }

        public void Actualizar(IncidenciaDto incidenciaDto)
        {
            var incidencia = AutoMapperConfig.Mapper.Map<Incidencia>(incidenciaDto);

            _incidenciaRepository.Actualizar(incidencia);
            _incidenciaRepository.Guardar();
        }

        public void Eliminar(int id)
        {
            _incidenciaRepository.Eliminar(id);
            _incidenciaRepository.Guardar();
        }
    }
}
