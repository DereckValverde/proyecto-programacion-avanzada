using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condominio.Application.Usuarios
{
    public interface IUsuarioService
    {

        IEnumerable<UsuarioDto> ObtenerTodos();

        UsuarioDto ObtenerPorId(int id);

        UsuarioDto Agregar(UsuarioDto usuarioDto);

        void Actualizar(UsuarioDto usuarioDto);

        void Eliminar(int id);

    }
}
