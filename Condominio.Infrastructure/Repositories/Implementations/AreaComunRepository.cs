using Condominio.Domain.Entities;
using Condominio.Infrastructure.DbContexts;
using Condominio.Application.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Condominio.Infrastructure.Repositories.Implementations
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

        public IEnumerable<AreaComun> ObtenerTodas()
        {
            return _context.AreasComunes.ToList();
        }
        }

        public AreaComun ObtenerPorId(int id)
        {
            return _context.AreasComunes.Find(id);
        }
    }
}