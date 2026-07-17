using System.Data.Entity;
using proyecto_programacion_avanzada.Entities;

namespace proyecto_programacion_avanzada.Infrastructure.DbContexts
{
    public class CondominioContext : DbContext
    {
        public CondominioContext()
            : base("CondominioConnection")
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Residente> Residentes { get; set; }
        public DbSet<Vivienda> Viviendas { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<AreaComun> AreasComunes { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Visitante> Visitantes { get; set; }
        public DbSet<Incidencia> Incidencias { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Usuario 1 - 0..1 Residente.
            // El índice único de IdUsuario garantiza que un usuario
            // no pueda asociarse con más de un residente.
            modelBuilder.Entity<Residente>()
                .HasRequired(residente => residente.Usuario)
                .WithMany()
                .HasForeignKey(residente => residente.IdUsuario)
                .WillCascadeOnDelete(false);

            // Vivienda 1 - N Residentes
            modelBuilder.Entity<Residente>()
                .HasRequired(residente => residente.Vivienda)
                .WithMany(vivienda => vivienda.Residentes)
                .HasForeignKey(residente => residente.IdVivienda)
                .WillCascadeOnDelete(false);

            // Vivienda 1 - N Pagos
            modelBuilder.Entity<Pago>()
                .HasRequired(pago => pago.Vivienda)
                .WithMany(vivienda => vivienda.Pagos)
                .HasForeignKey(pago => pago.IdVivienda)
                .WillCascadeOnDelete(false);

            // Vivienda 1 - N Reservas
            modelBuilder.Entity<Reserva>()
                .HasRequired(reserva => reserva.Vivienda)
                .WithMany(vivienda => vivienda.Reservas)
                .HasForeignKey(reserva => reserva.IdVivienda)
                .WillCascadeOnDelete(false);

            // Área común 1 - N Reservas
            modelBuilder.Entity<Reserva>()
                .HasRequired(reserva => reserva.AreaComun)
                .WithMany(area => area.Reservas)
                .HasForeignKey(reserva => reserva.IdArea)
                .WillCascadeOnDelete(false);

            // Vivienda 1 - N Visitantes
            modelBuilder.Entity<Visitante>()
                .HasRequired(visitante => visitante.Vivienda)
                .WithMany(vivienda => vivienda.Visitantes)
                .HasForeignKey(visitante => visitante.IdVivienda)
                .WillCascadeOnDelete(false);

            // Residente 1 - N Incidencias
            modelBuilder.Entity<Incidencia>()
                .HasRequired(incidencia => incidencia.Residente)
                .WithMany(residente => residente.Incidencias)
                .HasForeignKey(incidencia => incidencia.IdResidente)
                .WillCascadeOnDelete(false);

            // Configuración del decimal de Pago
            modelBuilder.Entity<Pago>()
                .Property(pago => pago.Monto)
                .HasPrecision(18, 2);

            // Índice único compuesto para Número + Bloque
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