using Condominio.Application.DTOs;
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

        public IEnumerable<AreaComunDto> ObtenerTodas()
        {
            var areas = _areaComunRepository.ObtenerTodas();

            return areas.Select(a => AutoMapperConfig.Mapper.Map<AreaComunDto>(a));
        }
    }
}