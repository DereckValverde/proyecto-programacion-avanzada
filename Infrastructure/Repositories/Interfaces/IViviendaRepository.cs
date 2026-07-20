using proyecto_programacion_avanzada.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_programacion_avanzada.Infrastructure.Repositories.Interfaces
{
    public interface IViviendaRepository
    {

        IEnumerable<Vivienda> ObtenerTodos();

        Vivienda ObtenerPorId(int id);

        void Agregar(Vivienda vivienda);

        void Actualizar(Vivienda vivienda);

        void Eliminar(int id);

        void Guardar();
    }
}
