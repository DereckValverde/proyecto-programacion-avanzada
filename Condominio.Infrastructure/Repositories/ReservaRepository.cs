using Condominio.Application.Reservas;
using Condominio.Domain.AreasComunes;
using Condominio.Domain.Reservas;
using Condominio.Domain.Viviendas;
using Condominio.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Condominio.Infrastructure.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly CondominioContext _context;

        public ReservaRepository(CondominioContext context)
        {
            _context = context;
        }

        public IEnumerable<Reserva> ObtenerTodos()
        {
            return _context.Reservas
                .Include(r => r.Vivienda)
                .Include(r => r.AreaComun)
                .OrderByDescending(r => r.FechaReserva)
                .ToList();
        }

        public IEnumerable<Reserva> ObtenerPorVivienda(int idVivienda)
        {
            return _context.Reservas
                .Include(r => r.Vivienda)
                .Include(r => r.AreaComun)
                .Where(r => r.IdVivienda == idVivienda)
                .OrderByDescending(r => r.FechaReserva)
                .ToList();
        }

        public Reserva ObtenerPorId(int id)
        {
            return _context.Reservas
                .Include(r => r.Vivienda)
                .Include(r => r.AreaComun)
                .FirstOrDefault(r => r.IdReserva == id);
        }

        public bool ExisteTraslape(int idArea, DateTime fechaReserva, TimeSpan horaInicio, TimeSpan horaFin, int idReservaExcluir = 0)
        {
            return _context.Reservas
                .Where(r => r.IdArea == idArea
                    && DbFunctions.TruncateTime(r.FechaReserva) == DbFunctions.TruncateTime(fechaReserva)
                    && r.Estado != EstadoReserva.Cancelada
                    && r.IdReserva != idReservaExcluir)
                .AsEnumerable()
                .Any(r => horaInicio < r.HoraFin && horaFin > r.HoraInicio);
        }

        public void Agregar(Reserva reserva)
        {
            _context.Reservas.Add(reserva);
        }

        public void Actualizar(Reserva reserva)
        {
            var reservaExistente = _context.Reservas.Find(reserva.IdReserva);

            if (reservaExistente == null)
            {
                return;
            }

            reservaExistente.FechaReserva = reserva.FechaReserva;
            reservaExistente.HoraInicio = reserva.HoraInicio;
            reservaExistente.HoraFin = reserva.HoraFin;
            reservaExistente.Estado = reserva.Estado;
            reservaExistente.IdVivienda = reserva.IdVivienda;
            reservaExistente.IdArea = reserva.IdArea;
        }

        public void Eliminar(int id)
        {
            var reserva = _context.Reservas.Find(id);

            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
            }
        }

        public void Guardar()
        {
            _context.SaveChanges();
        }
    }
}