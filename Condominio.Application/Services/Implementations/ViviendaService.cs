using Condominio.Application.DTOs;
using Condominio.Domain.Entities;
using Condominio.Application.Repositories.Interfaces;
using Condominio.Application.Mappings;
using Condominio.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Condominio.Application.Services.Implementations
{
    public class ViviendaService : IViviendaService
    {

        private readonly IViviendaRepository _viviendaRepository;

        public ViviendaService(IViviendaRepository viviendaRepository)
        {
            _viviendaRepository = viviendaRepository;
        }

        public void Actualizar(ViviendaDto viviendaDto)
        {
            var vivienda = AutoMapperConfig.Mapper.Map<Vivienda>(viviendaDto);

            _viviendaRepository.Actualizar(vivienda);
            _viviendaRepository.Guardar();

        }

        public void Agregar(ViviendaDto viviendaDto)
        {
            var vivienda = AutoMapperConfig.Mapper.Map<Vivienda>(viviendaDto);

            _viviendaRepository.Agregar(vivienda);
            _viviendaRepository.Guardar();
        }

        public void Eliminar(int id)
        {
            _viviendaRepository.Eliminar(id);
            _viviendaRepository.Guardar();
        }

        public ViviendaDto ObtenerPorId(int id)
        {
            var vivienda = _viviendaRepository.ObtenerPorId(id);

            if(vivienda == null)
            {
                return null;
            }

            return AutoMapperConfig.Mapper.Map<ViviendaDto>(vivienda);
        }

        public IEnumerable<ViviendaDto> obtenerTodos()
        {
            var viviendas = _viviendaRepository.ObtenerTodos();

            return viviendas.Select(vivienda =>
                AutoMapperConfig.Mapper.Map<ViviendaDto>(vivienda));
        }
    }
}