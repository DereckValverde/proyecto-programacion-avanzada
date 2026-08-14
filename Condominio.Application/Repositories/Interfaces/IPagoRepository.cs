using Condominio.Domain.Entities;
using Condominio.Domain.Enums;
using System.Collections.Generic;

namespace Condominio.Application.Repositories.Interfaces
{
    public interface IPagoRepository
    {
        IEnumerable<Pago> ObtenerTodos();

        Pago ObtenerPorId(int id);

        IEnumerable<Pago> ObtenerPorVivienda(int idVivienda);

        IEnumerable<Pago> ObtenerPorEstado(EstadoPago estado);

        bool ExisteParaViviendaYPeriodo(int idVivienda, string periodo, int? idExcluir);

        void Agregar(Pago pago);

        void Actualizar(Pago pago);

        void Eliminar(int id);

        void Guardar();
    }
}
