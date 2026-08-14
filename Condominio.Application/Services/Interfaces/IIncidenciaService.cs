using Condominio.Application.DTOs;
using System.Collections.Generic;

namespace Condominio.Application.Services.Interfaces
{
    public interface IIncidenciaService
    {
        IEnumerable<IncidenciaDto> ObtenerTodas();

        IEnumerable<IncidenciaDto> ObtenerPorResidente(int idResidente);

        IncidenciaDto ObtenerPorId(int id);

        void Agregar(IncidenciaDto incidenciaDto);

        void Actualizar(IncidenciaDto incidenciaDto);

        void Eliminar(int id);
    }
}
