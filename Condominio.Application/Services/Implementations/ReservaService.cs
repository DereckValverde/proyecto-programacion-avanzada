using Condominio.Application.DTOs;
using Condominio.Domain.Entities;
using Condominio.Application.Repositories.Interfaces;
using Condominio.Application.Mappings;
using Condominio.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Condominio.Application.Services.Implementations
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _reservaRepository;

        public ReservaService(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        public IEnumerable<ReservaDto> ObtenerTodos()
        {
            var reservas = _reservaRepository.ObtenerTodos();

            return reservas.Select(r => AutoMapperConfig.Mapper.Map<ReservaDto>(r));
        }

        public ReservaDto ObtenerPorId(int id)
        {
            var reserva = _reservaRepository.ObtenerPorId(id);

            return reserva == null
                ? null
                : AutoMapperConfig.Mapper.Map<ReservaDto>(reserva);
        }

        public void Agregar(ReservaDto reservaDto)
        {
            ValidarReserva(reservaDto);

            var reserva = AutoMapperConfig.Mapper.Map<Reserva>(reservaDto);

            _reservaRepository.Agregar(reserva);
            _reservaRepository.Guardar();
        }

        public void Actualizar(ReservaDto reservaDto)
        {
            ValidarReserva(reservaDto);

            var reserva = AutoMapperConfig.Mapper.Map<Reserva>(reservaDto);

            _reservaRepository.Actualizar(reserva);
            _reservaRepository.Guardar();
        }

        public void Eliminar(int id)
        {
            _reservaRepository.Eliminar(id);
            _reservaRepository.Guardar();
        }

        private void ValidarReserva(ReservaDto reservaDto)
        {
            if (reservaDto.HoraFin <= reservaDto.HoraInicio)
            {
                throw new InvalidOperationException(
                    "La hora de fin debe ser mayor a la hora de inicio.");
            }

            var hayTraslape = _reservaRepository.ExisteTraslape(
                reservaDto.IdArea,
                reservaDto.FechaReserva,
                reservaDto.HoraInicio,
                reservaDto.HoraFin,
                reservaDto.IdReserva);

            if (hayTraslape)
            {
                throw new InvalidOperationException(
                    "Ya existe una reserva para esa área común en ese horario.");
            }
        }
    }
}