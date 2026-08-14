using Condominio.Application.DTOs;
using System.Collections.Generic;

namespace Condominio.Application.Services.Interfaces
{
    public interface IAreaComunService
    {
        IEnumerable<AreaComunDto> ObtenerTodas();
    }
}