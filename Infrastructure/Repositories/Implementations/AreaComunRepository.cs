using proyecto_programacion_avanzada.Entities;
using proyecto_programacion_avanzada.Infrastructure.DbContexts;
using proyecto_programacion_avanzada.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace proyecto_programacion_avanzada.Infrastructure.Repositories.Implementations
{
    public class AreaComunRepository : IAreaComunRepository
    {
        private readonly CondominioContext _context;

        public AreaComunRepository(CondominioContext context)
        {
            _context = context;
        }

        public void Actualizar(AreaComun areaComun)
        {
            var areaExistente = _context.AreasComunes.Find(areaComun.IdArea);

            if (areaExistente == null)
            {
                return;
            }

            areaExistente.Nombre = areaComun.Nombre;
            areaExistente.Descripcion = areaComun.Descripcion;
            areaExistente.Capacidad = areaComun.Capacidad;
            areaExistente.HoraApertura = areaComun.HoraApertura;
            areaExistente.HoraCierre = areaComun.HoraCierre;
        }

        public void Agregar(AreaComun areaComun)
        {
            _context.AreasComunes.Add(areaComun);
        }

        public void Eliminar(int id)
        {
            var areaComun = _context.AreasComunes.Find(id);

            if (areaComun != null)
            {
                _context.AreasComunes.Remove(areaComun);
            }
        }

        public void Guardar()
        {
            _context.SaveChanges();
        }

        public AreaComun ObtenerPorId(int id)
        {
            return _context.AreasComunes.Find(id);
        }

        public IEnumerable<AreaComun> ObtenerTodos()
        {
            return _context.AreasComunes.ToList();
        }
    }
}