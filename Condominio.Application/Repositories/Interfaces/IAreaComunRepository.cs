using Condominio.Domain.Entities;
using System.Collections.Generic;

namespace Condominio.Application.Repositories.Interfaces
{
    public interface IAreaComunRepository
    {
        IEnumerable<AreaComun> ObtenerTodas();
        AreaComun ObtenerPorId(int id);
        void Agregar(AreaComun areaComun);
        void Actualizar(AreaComun areaComun);
        void Eliminar(int id);
        void Guardar();
    }
}
    }
}