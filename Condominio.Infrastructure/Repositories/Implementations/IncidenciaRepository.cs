using Condominio.Domain.Entities;
using Condominio.Infrastructure.DbContexts;
using Condominio.Application.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Condominio.Infrastructure.Repositories.Implementations
{
    public class IncidenciaRepository : IIncidenciaRepository
    {
        private readonly CondominioContext _context;

        public IncidenciaRepository(CondominioContext context)
        {
            _context = context;
        }

        public IEnumerable<Incidencia> ObtenerTodas()
        {
            return _context.Incidencias
                .Include(i => i.Residente)
                .Include(i => i.Residente.Vivienda)
                .OrderByDescending(i => i.FechaReporte)
                .ToList();
        }

        public IEnumerable<Incidencia> ObtenerPorResidente(int idResidente)
        {
            return _context.Incidencias
                .Include(i => i.Residente)
                .Include(i => i.Residente.Vivienda)
                .Where(i => i.IdResidente == idResidente)
                .OrderByDescending(i => i.FechaReporte)
                .ToList();
        }

        public Incidencia ObtenerPorId(int id)
        {
            return _context.Incidencias
                .Include(i => i.Residente)
                .Include(i => i.Residente.Vivienda)
                .FirstOrDefault(i => i.IdIncidencia == id);
        }

        public void Agregar(Incidencia incidencia)
        {
            _context.Incidencias.Add(incidencia);
        }

        public void Actualizar(Incidencia incidencia)
        {
            var incidenciaExistente = _context.Incidencias.Find(incidencia.IdIncidencia);

            if (incidenciaExistente == null)
            {
                return;
            }

            incidenciaExistente.Descripcion = incidencia.Descripcion;
            incidenciaExistente.Estado = incidencia.Estado;
            incidenciaExistente.Prioridad = incidencia.Prioridad;
        }

        public void Eliminar(int id)
        {
            var incidencia = _context.Incidencias.Find(id);

            if (incidencia != null)
            {
                _context.Incidencias.Remove(incidencia);
            }
        }

        public void Guardar()
        {
            _context.SaveChanges();
        }
    }
}
