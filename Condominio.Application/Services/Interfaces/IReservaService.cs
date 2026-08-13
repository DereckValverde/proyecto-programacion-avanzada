using Condominio.Application.DTOs;
using System.Collections.Generic;

namespace Condominio.Application.Services.Interfaces
{
    public interface IReservaService
    {
        IEnumerable<ReservaDto> ObtenerTodos();

        ReservaDto ObtenerPorId(int id);

        void Agregar(ReservaDto reservaDto);

        void Actualizar(ReservaDto reservaDto);

        void Eliminar(int id);
    }
}