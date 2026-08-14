using Condominio.Application.Noticias;
using Condominio.Domain.Noticias;
using Condominio.Infrastructure.DbContexts;
using System.Collections.Generic;
using System.Linq;

namespace Condominio.Infrastructure.Repositories
{
    public class NoticiaRepository : INoticiaRepository
    {
        private readonly CondominioContext _context;

        public NoticiaRepository(CondominioContext context)
        {
            _context = context;
        }

        public IEnumerable<Noticia> ObtenerTodas()
        {
            return _context.Noticias
                .OrderByDescending(n => n.FechaPublicacion)
                .ToList();
        }

        public Noticia ObtenerPorId(int id)
        {
            return _context.Noticias.Find(id);
        }

        public void Agregar(Noticia noticia)
        {
            _context.Noticias.Add(noticia);
        }

        public void Actualizar(Noticia noticia)
        {
            var noticiaExistente = _context.Noticias.Find(noticia.IdNoticia);

            if (noticiaExistente == null)
            {
                return;
            }

            noticiaExistente.Titulo = noticia.Titulo;
            noticiaExistente.Contenido = noticia.Contenido;
            noticiaExistente.EsAlerta = noticia.EsAlerta;
            noticiaExistente.Autor = noticia.Autor;
        }

        public void Eliminar(int id)
        {
            var noticia = _context.Noticias.Find(id);

            if (noticia != null)
            {
                _context.Noticias.Remove(noticia);
            }
        }

        public void Guardar()
        {
            _context.SaveChanges();
        }
    }
}
