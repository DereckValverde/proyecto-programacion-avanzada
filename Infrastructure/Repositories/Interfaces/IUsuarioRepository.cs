using proyecto_programacion_avanzada.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_programacion_avanzada.Infrastructure.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        IEnumerable<Usuario> ObtenerTodos();

        Usuario ObtenerPorId(int id);

        void Agregar(Usuario usuario);

        void Actualizar(Usuario usuario);

        void Eliminar(int id);

        void Guardar();
    }
}
