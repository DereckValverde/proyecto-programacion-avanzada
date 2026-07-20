using proyecto_programacion_avanzada.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_programacion_avanzada.Infrastructure.Repositories.Interfaces
{
    public interface IResidenteRepository
    {
        IEnumerable<Residente> ObtenerTodos();

        Residente ObtenerPorId(int id);

        void Agregar(Residente residente);

        void Actualizar(Residente residente);

        void Eliminar(int id);

        void Guardar();
    }
}
