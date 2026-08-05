using proyecto_programacion_avanzada.Entities;
using System.Collections.Generic;

namespace proyecto_programacion_avanzada.Infrastructure.Repositories.Interfaces
{
    public interface IAreaComunRepository
    {
        IEnumerable<AreaComun> ObtenerTodos();

        AreaComun ObtenerPorId(int id);

        void Agregar(AreaComun areaComun);

        void Actualizar(AreaComun areaComun);

        void Eliminar(int id);

        void Guardar();
    }
}