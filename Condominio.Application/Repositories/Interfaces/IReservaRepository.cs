using Condominio.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Condominio.Application.Repositories.Interfaces
{
    public interface IReservaRepository
    {
        IEnumerable<Reserva> ObtenerTodos();

        IEnumerable<Reserva> ObtenerPorVivienda(int idVivienda);

        Reserva ObtenerPorId(int id);

        bool ExisteTraslape(int idArea, DateTime fechaReserva, TimeSpan horaInicio, TimeSpan horaFin, int idReservaExcluir = 0);

        void Agregar(Reserva reserva);

        void Actualizar(Reserva reserva);

        void Eliminar(int id);

        void Guardar();
    }
}