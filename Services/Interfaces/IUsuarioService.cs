using proyecto_programacion_avanzada.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_programacion_avanzada.Services.Interfaces
{
    public interface IUsuarioService
    {

        IEnumerable<UsuarioDto> ObtenerTodos();

        UsuarioDto ObtenerPorId(int id);

        void Agregar(UsuarioDto usuarioDto);

        void Actualizar(UsuarioDto usuarioDto);

        void Eliminar(int id);

    }
}
