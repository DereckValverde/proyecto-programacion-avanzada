using System.Collections.Generic;

namespace Condominio.Application.AreasComunes
{
    public interface IAreaComunService
    {
        IEnumerable<AreaComunDto> ObtenerTodas();
    }
}