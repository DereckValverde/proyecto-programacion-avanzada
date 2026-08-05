using proyecto_programacion_avanzada.DTOs;
using proyecto_programacion_avanzada.Entities;
using proyecto_programacion_avanzada.Infrastructure.Repositories.Interfaces;
using proyecto_programacion_avanzada.Mappings;
using proyecto_programacion_avanzada.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace proyecto_programacion_avanzada.Services.Implementations
{
    public class AreaComunService : IAreaComunService
    {
        private readonly IAreaComunRepository _areaComunRepository;

        public AreaComunService(IAreaComunRepository areaComunRepository)
        {
            _areaComunRepository = areaComunRepository;
        }

        public void Actualizar(AreaComunDto areaComunDto)
        {
            var areaComun =
                AutoMapperConfig.Mapper.Map<AreaComun>(areaComunDto);

            _areaComunRepository.Actualizar(areaComun);
            _areaComunRepository.Guardar();
        }

        public AreaComunDto Agregar(AreaComunDto areaComunDto)
        {
            var areaComun =
                AutoMapperConfig.Mapper.Map<AreaComun>(areaComunDto);

            _areaComunRepository.Agregar(areaComun);
            _areaComunRepository.Guardar();

            return AutoMapperConfig.Mapper.Map<AreaComunDto>(areaComun);
        }

        public void Eliminar(int id)
        {
            _areaComunRepository.Eliminar(id);
            _areaComunRepository.Guardar();
        }

        public AreaComunDto ObtenerPorId(int id)
        {
            var areaComun = _areaComunRepository.ObtenerPorId(id);

            if (areaComun == null)
            {
                return null;
            }

            return AutoMapperConfig.Mapper.Map<AreaComunDto>(areaComun);
        }

        public IEnumerable<AreaComunDto> ObtenerTodos()
        {
            var areasComunes = _areaComunRepository.ObtenerTodos();

            return areasComunes.Select(areaComun =>
                AutoMapperConfig.Mapper.Map<AreaComunDto>(areaComun));
        }
    }
}