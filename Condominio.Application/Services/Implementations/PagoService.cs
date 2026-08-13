using Condominio.Application.DTOs;
using Condominio.Application.Mappings;
using Condominio.Application.Repositories.Interfaces;
using Condominio.Application.Services.Interfaces;
using Condominio.Domain.Entities;
using Condominio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Condominio.Application.Services.Implementations
{
    public class PagoService : IPagoService
    {
        private readonly IPagoRepository _pagoRepository;

        public PagoService(IPagoRepository pagoRepository)
        {
            _pagoRepository = pagoRepository;
        }

        public IEnumerable<PagoDto> ObtenerTodos()
        {
            var pagos = _pagoRepository.ObtenerTodos();

            return pagos.Select(p => AutoMapperConfig.Mapper.Map<PagoDto>(p));
        }

        public PagoDto ObtenerPorId(int id)
        {
            var pago = _pagoRepository.ObtenerPorId(id);

            return pago == null
                ? null
                : AutoMapperConfig.Mapper.Map<PagoDto>(pago);
        }

        public IEnumerable<PagoDto> ObtenerPorVivienda(int idVivienda)
        {
            var pagos = _pagoRepository.ObtenerPorVivienda(idVivienda);

            return pagos.Select(p => AutoMapperConfig.Mapper.Map<PagoDto>(p));
        }

        public IEnumerable<PagoDto> ObtenerPorEstado(EstadoPago estado)
        {
            var pagos = _pagoRepository.ObtenerPorEstado(estado);

            return pagos.Select(p => AutoMapperConfig.Mapper.Map<PagoDto>(p));
        }

        public void Agregar(PagoDto pagoDto)
        {
            if (_pagoRepository.ExisteParaViviendaYPeriodo(pagoDto.IdVivienda, pagoDto.Periodo, null))
            {
                throw new InvalidOperationException(
                    "Ya existe un pago registrado para esta vivienda en el periodo indicado.");
            }

            var pago = AutoMapperConfig.Mapper.Map<Pago>(pagoDto);

            _pagoRepository.Agregar(pago);
            _pagoRepository.Guardar();
        }

        public void Actualizar(PagoDto pagoDto)
        {
            if (_pagoRepository.ExisteParaViviendaYPeriodo(pagoDto.IdVivienda, pagoDto.Periodo, pagoDto.IdPago))
            {
                throw new InvalidOperationException(
                    "Ya existe otro pago registrado para esta vivienda en el periodo indicado.");
            }

            var pago = AutoMapperConfig.Mapper.Map<Pago>(pagoDto);

            _pagoRepository.Actualizar(pago);
            _pagoRepository.Guardar();
        }

        public void Eliminar(int id)
        {
            _pagoRepository.Eliminar(id);
            _pagoRepository.Guardar();
        }
    }
}
