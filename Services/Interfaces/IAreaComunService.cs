using proyecto_programacion_avanzada.DTOs;
using System.Collections.Generic;

namespace proyecto_programacion_avanzada.Services.Interfaces
{
    public interface IAreaComunService
    {
        IEnumerable<AreaComunDto> ObtenerTodos();

        AreaComunDto ObtenerPorId(int id);

        AreaComunDto Agregar(AreaComunDto areaComunDto);

        void Actualizar(AreaComunDto areaComunDto);

        void Eliminar(int id);
    }
}