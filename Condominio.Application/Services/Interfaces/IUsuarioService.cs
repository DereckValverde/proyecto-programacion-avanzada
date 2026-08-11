using Condominio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condominio.Application.Services.Interfaces
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
