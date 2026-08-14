using System.Collections.Generic;

namespace Condominio.Application.Noticias
{
    public interface INoticiaService
    {
        IEnumerable<NoticiaDto> ObtenerTodas();

        NoticiaDto ObtenerPorId(int id);

        void Agregar(NoticiaDto noticiaDto);

        void Actualizar(NoticiaDto noticiaDto);

        void Eliminar(int id);
    }
}
