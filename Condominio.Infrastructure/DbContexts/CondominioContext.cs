using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Condominio.Domain.Entities;

namespace Condominio.Infrastructure.DbContexts
{
    public class CondominioContext : IdentityDbContext<Usuario, ApplicationRole, int,
        ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public CondominioContext()
            : base("CondominioConnection")
        {
        }

        public DbSet<Residente> Residentes { get; set; }
        public DbSet<Vivienda> Viviendas { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<AreaComun> AreasComunes { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Visitante> Visitantes { get; set; }
        public DbSet<Incidencia> Incidencias { get; set; }
        public DbSet<Noticia> Noticias { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .Property(usuario => usuario.Id)
                .HasColumnName("IdUsuario");

            modelBuilder.Entity<Usuario>()
                .Property(usuario => usuario.UserName)
                .HasColumnName("Correo")
                .HasMaxLength(256)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(usuario => usuario.PasswordHash)
                .HasColumnName("Contrasena")
                .HasMaxLength(255);

            modelBuilder.Entity<Residente>()
                .HasRequired(residente => residente.Usuario)
                .WithMany()
                .HasForeignKey(residente => residente.IdUsuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Residente>()
                .HasRequired(residente => residente.Vivienda)
                .WithMany(vivienda => vivienda.Residentes)
                .HasForeignKey(residente => residente.IdVivienda)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pago>()
                .HasRequired(pago => pago.Vivienda)
                .WithMany(vivienda => vivienda.Pagos)
                .HasForeignKey(pago => pago.IdVivienda)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reserva>()
                .HasRequired(reserva => reserva.Vivienda)
                .WithMany(vivienda => vivienda.Reservas)
                .HasForeignKey(reserva => reserva.IdVivienda)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reserva>()
                .HasRequired(reserva => reserva.AreaComun)
                .WithMany(area => area.Reservas)
                .HasForeignKey(reserva => reserva.IdArea)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Visitante>()
                .HasRequired(visitante => visitante.Vivienda)
                .WithMany(vivienda => vivienda.Visitantes)
                .HasForeignKey(visitante => visitante.IdVivienda)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Incidencia>()
                .HasRequired(incidencia => incidencia.Residente)
                .WithMany(residente => residente.Incidencias)
                .HasForeignKey(incidencia => incidencia.IdResidente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pago>()
                .Property(pago => pago.Monto)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Vivienda>()
                .Property(vivienda => vivienda.Numero)
                .HasColumnAnnotation(
                    "Index",
                    new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation(
                        new System.ComponentModel.DataAnnotations.Schema.IndexAttribute(
                            "IX_Vivienda_Numero_Bloque",
                            1)
                        {
                            IsUnique = true
                        }));

            modelBuilder.Entity<Vivienda>()
                .Property(vivienda => vivienda.Bloque)
                .HasColumnAnnotation(
                    "Index",
                    new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation(
                        new System.ComponentModel.DataAnnotations.Schema.IndexAttribute(
                            "IX_Vivienda_Numero_Bloque",
                            2)
                        {
                            IsUnique = true
                        }));
        }
    }
}