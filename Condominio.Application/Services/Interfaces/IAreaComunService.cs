using Condominio.Application.DTOs;
using System.Collections.Generic;

namespace Condominio.Application.Services.Interfaces
{
    public interface IAreaComunService
    {
        IEnumerable<AreaComunDto> ObtenerTodas();

        AreaComunDto ObtenerPorId(int id);

        AreaComunDto Agregar(AreaComunDto areaComunDto);

        void Actualizar(AreaComunDto areaComunDto);

        void Eliminar(int id);
    }
}
    }
}