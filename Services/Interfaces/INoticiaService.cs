using proyecto_programacion_avanzada.DTOs;
using System.Collections.Generic;

namespace proyecto_programacion_avanzada.Services.Interfaces
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
