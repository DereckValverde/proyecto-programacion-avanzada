using Condominio.Application.Mappings;
using Condominio.Domain.Visitantes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Condominio.Application.Visitantes
{
    public class VisitanteService : IVisitanteService
    {
        private readonly IVisitanteRepository _visitanteRepository;

        public VisitanteService(IVisitanteRepository visitanteRepository)
        {
            _visitanteRepository = visitanteRepository;
        }

        public IEnumerable<VisitanteDto> ObtenerTodos()
        {
            var visitantes = _visitanteRepository.ObtenerTodos();

            return visitantes.Select(v => AutoMapperConfig.Mapper.Map<VisitanteDto>(v));
        }

        public VisitanteDto ObtenerPorId(int id)
        {
            var visitante = _visitanteRepository.ObtenerPorId(id);

            return visitante == null
                ? null
                : AutoMapperConfig.Mapper.Map<VisitanteDto>(visitante);
        }

        public IEnumerable<VisitanteDto> ObtenerHistorialPorVivienda(int idVivienda)
        {
            var visitantes = _visitanteRepository.ObtenerPorVivienda(idVivienda);

            return visitantes.Select(v => AutoMapperConfig.Mapper.Map<VisitanteDto>(v));
        }

        public IEnumerable<VisitanteDto> ObtenerActivos()
        {
            var visitantes = _visitanteRepository.ObtenerActivos();

            return visitantes.Select(v => AutoMapperConfig.Mapper.Map<VisitanteDto>(v));
        }

        public bool ExisteVisitanteActivoConIdentificacion(string identificacion)
        {
            return _visitanteRepository.ObtenerActivoPorIdentificacion(identificacion) != null;
        }

        public void RegistrarIngreso(VisitanteDto visitanteDto)
        {
            if (ExisteVisitanteActivoConIdentificacion(visitanteDto.Identificacion))
            {
                throw new InvalidOperationException(
                    "Ya existe un visitante con esta identificación que se encuentra dentro del condominio.");
            }

            visitanteDto.FechaIngreso = visitanteDto.FechaIngreso == default(DateTime)
                ? DateTime.Now
                : visitanteDto.FechaIngreso;

            visitanteDto.FechaSalida = null;

            var visitante = AutoMapperConfig.Mapper.Map<Visitante>(visitanteDto);

            _visitanteRepository.Agregar(visitante);
            _visitanteRepository.Guardar();
        }

        public void RegistrarSalida(int id)
        {
            var visitante = _visitanteRepository.ObtenerPorId(id);

            if (visitante == null)
            {
                throw new InvalidOperationException("El visitante indicado no existe.");
            }

            if (visitante.FechaSalida != null)
            {
                throw new InvalidOperationException("El visitante ya tiene registrada su salida.");
            }

            visitante.FechaSalida = DateTime.Now;

            _visitanteRepository.Actualizar(visitante);
            _visitanteRepository.Guardar();
        }

        public void Actualizar(VisitanteDto visitanteDto)
        {
            if (visitanteDto.FechaSalida.HasValue &&
                visitanteDto.FechaSalida.Value < visitanteDto.FechaIngreso)
            {
                throw new InvalidOperationException(
                    "La fecha de salida no puede ser anterior a la fecha de ingreso.");
            }

            var visitante = AutoMapperConfig.Mapper.Map<Visitante>(visitanteDto);

            _visitanteRepository.Actualizar(visitante);
            _visitanteRepository.Guardar();
        }

        public void Eliminar(int id)
        {
            _visitanteRepository.Eliminar(id);
            _visitanteRepository.Guardar();
        }
    }
}
