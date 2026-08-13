using Condominio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condominio.Application.Services.Interfaces
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
