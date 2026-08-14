using Condominio.Application.AreasComunes;
using Condominio.Domain.AreasComunes;
using Condominio.Infrastructure.DbContexts;
using System.Collections.Generic;
using System.Linq;

namespace Condominio.Infrastructure.Repositories
{
    public class AreaComunRepository : IAreaComunRepository
    {
        private readonly CondominioContext _context;

        public AreaComunRepository(CondominioContext context)
        {
            _context = context;
        }

        public IEnumerable<AreaComun> ObtenerTodas()
        {
            return _context.AreasComunes.ToList();
        }

        public AreaComun ObtenerPorId(int id)
        {
            return _context.AreasComunes.Find(id);
        }
    }
}