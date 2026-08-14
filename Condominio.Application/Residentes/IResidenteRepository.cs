using Condominio.Domain.Residentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condominio.Application.Residentes
{
    public interface IResidenteRepository
    {
        IEnumerable<Residente> ObtenerTodos();

        Residente ObtenerPorId(int id);

        Residente ObtenerPorIdUsuario(int idUsuario);

        void Agregar(Residente residente);

        void Actualizar(Residente residente);

        void Eliminar(int id);

        void Guardar();
    }
}
