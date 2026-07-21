using proyecto_programacion_avanzada.Entities;
using proyecto_programacion_avanzada.Infrastructure.DbContexts;
using proyecto_programacion_avanzada.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyecto_programacion_avanzada.Infrastructure.Repositories.Implementations
{
    public class ViviendaRepository : IViviendaRepository
    {

        private readonly CondominioContext _context;

        public ViviendaRepository(CondominioContext context)
        {
            _context = context;
        }

        public void Actualizar(Vivienda vivienda)
        {
            var viviendaExistente = _context.Viviendas.Find(vivienda.IdVivienda);

            if(viviendaExistente == null)
            {
                return;
            }

            viviendaExistente.Numero = vivienda.Numero;
            viviendaExistente.Bloque = vivienda.Bloque;
            viviendaExistente.Tipo = vivienda.Tipo;
            viviendaExistente.Estado = vivienda.Estado;

        }

        public void Agregar(Vivienda vivienda)
        {
            _context.Viviendas.Add(vivienda);
        }

        public void Eliminar(int id)
        {
            var vivienda = _context.Viviendas.Find(id);

            if(vivienda != null)
            {
                _context.Viviendas.Remove(vivienda);
            }
        }

        public void Guardar()
        {
            _context.SaveChanges();
        }

        public Vivienda ObtenerPorId(int id)
        {
            return _context.Viviendas.Find(id);
        }

        public IEnumerable<Vivienda> ObtenerTodos()
        {
            return _context.Viviendas.ToList();
        }
    }
}