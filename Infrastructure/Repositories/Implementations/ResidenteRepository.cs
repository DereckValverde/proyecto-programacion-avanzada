using proyecto_programacion_avanzada.Entities;
using proyecto_programacion_avanzada.Infrastructure.DbContexts;
using proyecto_programacion_avanzada.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace proyecto_programacion_avanzada.Infrastructure.Repositories.Implementations
{
    public class ResidenteRepository : IResidenteRepository
    {

        private readonly CondominioContext _context;

        public ResidenteRepository(CondominioContext context)
        {
            _context = context;
        }

        public void Actualizar(Residente residente)
        {
            var residenteExistente = _context.Residentes.Find(residente.IdResidente);

            if(residenteExistente == null)
            {
                return;
            }

            residenteExistente.Nombre = residente.Nombre;
            residenteExistente.FechaIngreso = residente.FechaIngreso;
            residenteExistente.Estado = residente.Estado;
            residenteExistente.IdUsuario = residente.IdUsuario;
            residenteExistente.IdVivienda = residente.IdVivienda;
        }

        public void Agregar(Residente residente)
        {
            _context.Residentes.Add(residente);
        }

        public void Eliminar(int id)
        {
            var residente = _context.Residentes.Find(id);

            if(residente != null)
            {
                _context.Residentes.Remove(residente);
            }
        }

        public void Guardar()
        {
            _context.SaveChanges();
        }

        public Residente ObtenerPorId(int id)
        {
            return _context.Residentes
                .Include(r => r.Usuario)
                .Include(r => r.Vivienda)
                .FirstOrDefault(r => r.IdResidente == id);
        }

        public IEnumerable<Residente> ObtenerTodos()
        {
            return _context.Residentes
                .Include(r => r.Usuario)
                .Include(r => r.Vivienda)
                .ToList();

        }
    }
}