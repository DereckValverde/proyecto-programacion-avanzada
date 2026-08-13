using Condominio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condominio.Application.Repositories.Interfaces
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
