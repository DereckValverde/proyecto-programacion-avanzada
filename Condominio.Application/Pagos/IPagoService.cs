using Condominio.Domain.Pagos;
using System.Collections.Generic;

namespace Condominio.Application.Pagos
{
    public interface IPagoService
    {
        IEnumerable<PagoDto> ObtenerTodos();

        PagoDto ObtenerPorId(int id);

        IEnumerable<PagoDto> ObtenerPorVivienda(int idVivienda);

        IEnumerable<PagoDto> ObtenerPorEstado(EstadoPago estado);

        void Agregar(PagoDto pagoDto);

        void Actualizar(PagoDto pagoDto);

        void Eliminar(int id);
    }
}
