using proyecto_programacion_avanzada.Entities;
using System.Collections.Generic;

namespace proyecto_programacion_avanzada.Infrastructure.Repositories.Interfaces
{
    public interface INoticiaRepository
    {
        IEnumerable<Noticia> ObtenerTodas();

        Noticia ObtenerPorId(int id);

        void Agregar(Noticia noticia);

        void Actualizar(Noticia noticia);

        void Eliminar(int id);

        void Guardar();
    }
}
