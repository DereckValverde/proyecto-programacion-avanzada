using proyecto_programacion_avanzada.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_programacion_avanzada.Services.Interfaces
{
    internal interface IViviendaService
    {

        IEnumerable<ViviendaDto> obtenerTodos();

        ViviendaDto ObtenerPorId(int id);

        void Agregar(ViviendaDto viviendaDto);

        void Actualizar(ViviendaDto viviendaDto);

        void Eliminar(int id);
    }
}
