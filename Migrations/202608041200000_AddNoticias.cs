namespace proyecto_programacion_avanzada.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddNoticias : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Noticias",
                c => new
                    {
                        IdNoticia = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false, maxLength: 150),
                        Contenido = c.String(nullable: false, maxLength: 2000),
                        FechaPublicacion = c.DateTime(nullable: false),
                        EsAlerta = c.Boolean(nullable: false),
                        Autor = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.IdNoticia);
        }

        public override void Down()
        {
            DropTable("dbo.Noticias");
        }
    }
}
