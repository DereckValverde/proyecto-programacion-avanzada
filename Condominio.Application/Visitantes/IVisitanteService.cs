using System.Collections.Generic;

namespace Condominio.Application.Visitantes
{
    public interface IVisitanteService
    {
        IEnumerable<VisitanteDto> ObtenerTodos();

        VisitanteDto ObtenerPorId(int id);

        IEnumerable<VisitanteDto> ObtenerHistorialPorVivienda(int idVivienda);

        IEnumerable<VisitanteDto> ObtenerActivos();

        bool ExisteVisitanteActivoConIdentificacion(string identificacion);

        void RegistrarIngreso(VisitanteDto visitanteDto);

        void RegistrarSalida(int id);

        void Actualizar(VisitanteDto visitanteDto);

        void Eliminar(int id);
    }
}
