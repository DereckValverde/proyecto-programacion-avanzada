using Condominio.Domain.Entities;
using System.Collections.Generic;

namespace Condominio.Application.Repositories.Interfaces
{
    public interface IAreaComunRepository
    {
        IEnumerable<AreaComun> ObtenerTodas();
        AreaComun ObtenerPorId(int id);
    }
}