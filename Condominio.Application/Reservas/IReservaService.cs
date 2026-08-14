using System.Collections.Generic;

namespace Condominio.Application.Reservas
{
    public interface IReservaService
    {
        IEnumerable<ReservaDto> ObtenerTodos();

        IEnumerable<ReservaDto> ObtenerPorVivienda(int idVivienda);

        ReservaDto ObtenerPorId(int id);

        void Agregar(ReservaDto reservaDto);

        void Actualizar(ReservaDto reservaDto);

        void Eliminar(int id);
    }
}