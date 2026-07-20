using proyecto_programacion_avanzada.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_programacion_avanzada.Services.Interfaces
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
