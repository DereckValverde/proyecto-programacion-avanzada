using Condominio.Domain.Entities;
using System.Collections.Generic;

namespace Condominio.Application.Repositories.Interfaces
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
