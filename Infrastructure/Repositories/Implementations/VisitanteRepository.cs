using proyecto_programacion_avanzada.Entities;
using proyecto_programacion_avanzada.Infrastructure.DbContexts;
using proyecto_programacion_avanzada.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace proyecto_programacion_avanzada.Infrastructure.Repositories.Implementations
{
    public class VisitanteRepository : IVisitanteRepository
    {
        private readonly CondominioContext _context;

        public VisitanteRepository(CondominioContext context)
        {
            _context = context;
        }

        public IEnumerable<Visitante> ObtenerTodos()
        {
            return _context.Visitantes
                .Include(v => v.Vivienda)
                .OrderByDescending(v => v.FechaIngreso)
                .ToList();
        }

        public Visitante ObtenerPorId(int id)
        {
            return _context.Visitantes
                .Include(v => v.Vivienda)
                .FirstOrDefault(v => v.IdVisitante == id);
        }

        public IEnumerable<Visitante> ObtenerPorVivienda(int idVivienda)
        {
            return _context.Visitantes
                .Include(v => v.Vivienda)
                .Where(v => v.IdVivienda == idVivienda)
                .OrderByDescending(v => v.FechaIngreso)
                .ToList();
        }

        public IEnumerable<Visitante> ObtenerActivos()
        {
            return _context.Visitantes
                .Include(v => v.Vivienda)
                .Where(v => v.FechaSalida == null)
                .OrderByDescending(v => v.FechaIngreso)
                .ToList();
        }

        public Visitante ObtenerActivoPorIdentificacion(string identificacion)
        {
            return _context.Visitantes
                .FirstOrDefault(v => v.Identificacion == identificacion && v.FechaSalida == null);
        }

        public void Agregar(Visitante visitante)
        {
            _context.Visitantes.Add(visitante);
        }

        public void Actualizar(Visitante visitante)
        {
            var visitanteExistente = _context.Visitantes.Find(visitante.IdVisitante);

            if (visitanteExistente == null)
            {
                return;
            }

            visitanteExistente.Nombre = visitante.Nombre;
            visitanteExistente.Identificacion = visitante.Identificacion;
            visitanteExistente.Tipo = visitante.Tipo;
            visitanteExistente.FechaIngreso = visitante.FechaIngreso;
            visitanteExistente.FechaSalida = visitante.FechaSalida;
            visitanteExistente.IdVivienda = visitante.IdVivienda;
        }

        public void Eliminar(int id)
        {
            var visitante = _context.Visitantes.Find(id);

            if (visitante != null)
            {
                _context.Visitantes.Remove(visitante);
            }
        }

        public void Guardar()
        {
            _context.SaveChanges();
        }
    }
}
