using proyecto_programacion_avanzada.DTOs;
using proyecto_programacion_avanzada.Entities;
using proyecto_programacion_avanzada.Infrastructure.Repositories.Implementations;
using proyecto_programacion_avanzada.Infrastructure.Repositories.Interfaces;
using proyecto_programacion_avanzada.Mappings;
using proyecto_programacion_avanzada.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyecto_programacion_avanzada.Services.Implementations
{
    public class ResidenteService : IResidenteService
    {

        private readonly IResidenteRepository _residenteRepository;

        public ResidenteService(IResidenteRepository residenteRepository)
        {
            _residenteRepository = residenteRepository;
        }

        public void Actualizar(ResidenteDto residenteDto)
        {
            var residente = AutoMapperConfig.Mapper.Map<Residente>(residenteDto);

            _residenteRepository.Actualizar(residente);
            _residenteRepository.Guardar();
        }
            
        public void Agregar(ResidenteDto residenteDto)
        {
            var residente = AutoMapperConfig.Mapper.Map<Residente>(residenteDto);

            _residenteRepository.Agregar(residente);
            _residenteRepository.Guardar();
        }

        public void Eliminar(int id)
        {
            _residenteRepository.Eliminar(id);
            _residenteRepository.Guardar();
        }

        public ResidenteDto ObtenerPorId(int id)
        {
            var residente = _residenteRepository.ObtenerPorId(id);

            return residente == null
                ? null
                : AutoMapperConfig.Mapper.Map<ResidenteDto>(residente);
        }

        public IEnumerable<ResidenteDto> ObtenerTodos()
        {
            var residentes = _residenteRepository.ObtenerTodos();

            return residentes.Select(r =>
                AutoMapperConfig.Mapper.Map<ResidenteDto>(r));
        }
    }
}