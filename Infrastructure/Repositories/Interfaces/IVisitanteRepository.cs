using proyecto_programacion_avanzada.Entities;
using System.Collections.Generic;

namespace proyecto_programacion_avanzada.Infrastructure.Repositories.Interfaces
{
    public interface IVisitanteRepository
    {
        IEnumerable<Visitante> ObtenerTodos();

        Visitante ObtenerPorId(int id);

        IEnumerable<Visitante> ObtenerPorVivienda(int idVivienda);

        IEnumerable<Visitante> ObtenerActivos();

        Visitante ObtenerActivoPorIdentificacion(string identificacion);

        void Agregar(Visitante visitante);

        void Actualizar(Visitante visitante);

        void Eliminar(int id);

        void Guardar();
    }
}
