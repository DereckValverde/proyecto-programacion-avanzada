using Condominio.Domain.AreasComunes;
using System.Collections.Generic;

namespace Condominio.Application.AreasComunes
{
    public interface IAreaComunRepository
    {
        IEnumerable<AreaComun> ObtenerTodas();
        AreaComun ObtenerPorId(int id);
    }
}