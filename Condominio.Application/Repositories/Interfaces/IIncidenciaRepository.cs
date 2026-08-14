using Condominio.Domain.Entities;
using System.Collections.Generic;

namespace Condominio.Application.Repositories.Interfaces
{
    public interface IIncidenciaRepository
    {
        IEnumerable<Incidencia> ObtenerTodas();

        IEnumerable<Incidencia> ObtenerPorResidente(int idResidente);

        Incidencia ObtenerPorId(int id);

        void Agregar(Incidencia incidencia);

        void Actualizar(Incidencia incidencia);

        void Eliminar(int id);

        void Guardar();
    }
}
