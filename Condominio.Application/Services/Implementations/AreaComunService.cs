using Condominio.Application.DTOs;
using Condominio.Domain.Entities;
using Condominio.Application.Repositories.Interfaces;
using Condominio.Application.Mappings;
using Condominio.Application.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Condominio.Application.Services.Implementations
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
            var areaComun = AutoMapperConfig.Mapper.Map<AreaComun>(areaComunDto);

            _areaComunRepository.Actualizar(areaComun);
            _areaComunRepository.Guardar();
        }

        public AreaComunDto Agregar(AreaComunDto areaComunDto)
        {
            var areaComun = AutoMapperConfig.Mapper.Map<AreaComun>(areaComunDto);

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

        public IEnumerable<AreaComunDto> ObtenerTodas()
        {
            var areas = _areaComunRepository.ObtenerTodas();

            return areas.Select(a => AutoMapperConfig.Mapper.Map<AreaComunDto>(a));
        }
        }
    }
}