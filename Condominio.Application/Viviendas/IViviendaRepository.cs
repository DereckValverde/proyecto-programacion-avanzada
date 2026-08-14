using Condominio.Domain.Viviendas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condominio.Application.Viviendas
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
