using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condominio.Application.Residentes
{
    public interface IResidenteService
    {
        IEnumerable<ResidenteDto> ObtenerTodos();

        ResidenteDto ObtenerPorId(int id);

        void Agregar(ResidenteDto residenteDto);

        void Actualizar(ResidenteDto residenteDto);

        void Eliminar(int id);
    }
}
