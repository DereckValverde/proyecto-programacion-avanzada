using Condominio.Application.Mappings;
using Condominio.Domain.Residentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Condominio.Application.Residentes
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