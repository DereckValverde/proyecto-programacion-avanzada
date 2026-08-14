using Condominio.Domain.Visitantes;
using System.Collections.Generic;

namespace Condominio.Application.Visitantes
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
