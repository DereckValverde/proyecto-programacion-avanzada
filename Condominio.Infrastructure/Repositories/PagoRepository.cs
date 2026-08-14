using Condominio.Application.Pagos;
using Condominio.Domain.Pagos;
using Condominio.Domain.Viviendas;
using Condominio.Infrastructure.DbContexts;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Condominio.Infrastructure.Repositories
{
    public class PagoRepository : IPagoRepository
    {
        private readonly CondominioContext _context;

        public PagoRepository(CondominioContext context)
        {
            _context = context;
        }

        public IEnumerable<Pago> ObtenerTodos()
        {
            return _context.Pagos
                .Include(p => p.Vivienda)
                .OrderByDescending(p => p.FechaPago)
                .ToList();
        }

        public Pago ObtenerPorId(int id)
        {
            return _context.Pagos
                .Include(p => p.Vivienda)
                .FirstOrDefault(p => p.IdPago == id);
        }

        public IEnumerable<Pago> ObtenerPorVivienda(int idVivienda)
        {
            return _context.Pagos
                .Include(p => p.Vivienda)
                .Where(p => p.IdVivienda == idVivienda)
                .OrderByDescending(p => p.FechaPago)
                .ToList();
        }

        public IEnumerable<Pago> ObtenerPorEstado(EstadoPago estado)
        {
            return _context.Pagos
                .Include(p => p.Vivienda)
                .Where(p => p.Estado == estado)
                .OrderByDescending(p => p.FechaPago)
                .ToList();
        }

        public bool ExisteParaViviendaYPeriodo(int idVivienda, string periodo, int? idExcluir)
        {
            return _context.Pagos
                .Any(p => p.IdVivienda == idVivienda
                    && p.Periodo == periodo
                    && (!idExcluir.HasValue || p.IdPago != idExcluir.Value));
        }

        public void Agregar(Pago pago)
        {
            _context.Pagos.Add(pago);
        }

        public void Actualizar(Pago pago)
        {
            var pagoExistente = _context.Pagos.Find(pago.IdPago);

            if (pagoExistente == null)
            {
                return;
            }

            pagoExistente.FechaPago = pago.FechaPago;
            pagoExistente.Monto = pago.Monto;
            pagoExistente.Periodo = pago.Periodo;
            pagoExistente.Estado = pago.Estado;
            pagoExistente.IdVivienda = pago.IdVivienda;
        }

        public void Eliminar(int id)
        {
            var pago = _context.Pagos.Find(id);

            if (pago != null)
            {
                _context.Pagos.Remove(pago);
            }
        }

        public void Guardar()
        {
            _context.SaveChanges();
        }
    }
}
