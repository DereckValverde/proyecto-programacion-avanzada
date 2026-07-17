namespace proyecto_programacion_avanzada.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AreasComunes",
                c => new
                    {
                        IdArea = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Descripcion = c.String(maxLength: 250),
                        Capacidad = c.Int(nullable: false),
                        HoraApertura = c.Time(nullable: false, precision: 7),
                        HoraCierre = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.IdArea);
            
            CreateTable(
                "dbo.Reservas",
                c => new
                    {
                        IdReserva = c.Int(nullable: false, identity: true),
                        FechaReserva = c.DateTime(nullable: false),
                        HoraInicio = c.Time(nullable: false, precision: 7),
                        HoraFin = c.Time(nullable: false, precision: 7),
                        Estado = c.Int(nullable: false),
                        IdVivienda = c.Int(nullable: false),
                        IdArea = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdReserva)
                .ForeignKey("dbo.AreasComunes", t => t.IdArea)
                .ForeignKey("dbo.Viviendas", t => t.IdVivienda)
                .Index(t => t.IdVivienda)
                .Index(t => t.IdArea);
            
            CreateTable(
                "dbo.Viviendas",
                c => new
                    {
                        IdVivienda = c.Int(nullable: false, identity: true),
                        Numero = c.String(nullable: false, maxLength: 20),
                        Bloque = c.String(nullable: false, maxLength: 20),
                        Tipo = c.Int(nullable: false),
                        Estado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdVivienda)
                .Index(t => new { t.Numero, t.Bloque }, unique: true, name: "IX_Vivienda_Numero_Bloque");
            
            CreateTable(
                "dbo.Pagos",
                c => new
                    {
                        IdPago = c.Int(nullable: false, identity: true),
                        FechaPago = c.DateTime(nullable: false),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Periodo = c.String(nullable: false, maxLength: 20),
                        Estado = c.Int(nullable: false),
                        IdVivienda = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdPago)
                .ForeignKey("dbo.Viviendas", t => t.IdVivienda)
                .Index(t => t.IdVivienda);
            
            CreateTable(
                "dbo.Residentes",
                c => new
                    {
                        IdResidente = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        FechaIngreso = c.DateTime(nullable: false),
                        Estado = c.Int(nullable: false),
                        IdUsuario = c.Int(nullable: false),
                        IdVivienda = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdResidente)
                .ForeignKey("dbo.Usuarios", t => t.IdUsuario)
                .ForeignKey("dbo.Viviendas", t => t.IdVivienda)
                .Index(t => t.IdUsuario, unique: true, name: "IX_Residente_IdUsuario")
                .Index(t => t.IdVivienda);
            
            CreateTable(
                "dbo.Incidencias",
                c => new
                    {
                        IdIncidencia = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 500),
                        FechaReporte = c.DateTime(nullable: false),
                        Estado = c.Int(nullable: false),
                        Prioridad = c.Int(nullable: false),
                        IdResidente = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdIncidencia)
                .ForeignKey("dbo.Residentes", t => t.IdResidente)
                .Index(t => t.IdResidente);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        IdUsuario = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Correo = c.String(nullable: false, maxLength: 150),
                        Telefono = c.String(maxLength: 20),
                        Contrasena = c.String(nullable: false, maxLength: 255),
                        Rol = c.Int(nullable: false),
                        Estado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdUsuario)
                .Index(t => t.Correo, unique: true, name: "IX_Usuario_Correo");
            
            CreateTable(
                "dbo.Visitantes",
                c => new
                    {
                        IdVisitante = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Identificacion = c.String(nullable: false, maxLength: 50),
                        Tipo = c.Int(nullable: false),
                        FechaIngreso = c.DateTime(nullable: false),
                        FechaSalida = c.DateTime(),
                        IdVivienda = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdVisitante)
                .ForeignKey("dbo.Viviendas", t => t.IdVivienda)
                .Index(t => t.Identificacion, unique: true, name: "IX_Visitante_Identificacion")
                .Index(t => t.IdVivienda);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservas", "IdVivienda", "dbo.Viviendas");
            DropForeignKey("dbo.Visitantes", "IdVivienda", "dbo.Viviendas");
            DropForeignKey("dbo.Residentes", "IdVivienda", "dbo.Viviendas");
            DropForeignKey("dbo.Residentes", "IdUsuario", "dbo.Usuarios");
            DropForeignKey("dbo.Incidencias", "IdResidente", "dbo.Residentes");
            DropForeignKey("dbo.Pagos", "IdVivienda", "dbo.Viviendas");
            DropForeignKey("dbo.Reservas", "IdArea", "dbo.AreasComunes");
            DropIndex("dbo.Visitantes", new[] { "IdVivienda" });
            DropIndex("dbo.Visitantes", "IX_Visitante_Identificacion");
            DropIndex("dbo.Usuarios", "IX_Usuario_Correo");
            DropIndex("dbo.Incidencias", new[] { "IdResidente" });
            DropIndex("dbo.Residentes", new[] { "IdVivienda" });
            DropIndex("dbo.Residentes", "IX_Residente_IdUsuario");
            DropIndex("dbo.Pagos", new[] { "IdVivienda" });
            DropIndex("dbo.Viviendas", "IX_Vivienda_Numero_Bloque");
            DropIndex("dbo.Reservas", new[] { "IdArea" });
            DropIndex("dbo.Reservas", new[] { "IdVivienda" });
            DropTable("dbo.Visitantes");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Incidencias");
            DropTable("dbo.Residentes");
            DropTable("dbo.Pagos");
            DropTable("dbo.Viviendas");
            DropTable("dbo.Reservas");
            DropTable("dbo.AreasComunes");
        }
    }
}
