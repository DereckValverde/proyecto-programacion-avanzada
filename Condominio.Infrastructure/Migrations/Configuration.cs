namespace Condominio.Infrastructure.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Condominio.Domain.Identity;
    using Condominio.Domain.Noticias;
    using Condominio.Domain.Residentes;
    using Condominio.Domain.Shared;
    using Condominio.Domain.Usuarios;
    using Condominio.Domain.Viviendas;
    using Condominio.Infrastructure.DbContexts;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration
        : DbMigrationsConfiguration<CondominioContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CondominioContext context)
        {
            CrearRoles(context);

            CrearUsuarioAdministrador(context);

            CrearViviendas(context);

            CrearNoticias(context);
        }

        private static void CrearRoles(CondominioContext context)
        {
            var roleManager = new RoleManager<ApplicationRole, int>(
                new RoleStore<ApplicationRole, int, ApplicationUserRole>(context)
            );

            string[] roles =
            {
                "Administrador",
                "Residente",
                "Guarda"
            };

            foreach (string nombreRol in roles)
            {
                if (!roleManager.RoleExists(nombreRol))
                {
                    var resultado = roleManager.Create(
                        new ApplicationRole { Name = nombreRol }
                    );

                    if (!resultado.Succeeded)
                    {
                        string errores = string.Join(
                            ", ",
                            resultado.Errors
                        );

                        throw new InvalidOperationException(
                            "No fue posible crear el rol "
                            + nombreRol
                            + ": "
                            + errores
                        );
                    }
                }
            }
        }

        private static void CrearUsuarioAdministrador(CondominioContext context)
        {
            var userManager = new UserManager<Usuario, int>(
                new UserStore<Usuario, ApplicationRole, int,
                    ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(context));

            userManager.UserValidator = new UserValidator<Usuario, int>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };

            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };

            const string correoAdministrador =
                "admin@condominio.com";

            const string contrasenaAdministrador =
                "Admin123*";

            var administrador =
                userManager.FindByName(correoAdministrador);

            if (administrador == null)
            {
                administrador = new Usuario
                {
                    UserName = correoAdministrador,
                    Nombre = "Administrador del Sistema",
                    Rol = RolUsuario.Administrador,
                    Estado = EstadoGeneral.Activo
                };

                var resultadoCreacion =
                    userManager.Create(
                        administrador,
                        contrasenaAdministrador
                    );

                if (!resultadoCreacion.Succeeded)
                {
                    string errores = string.Join(
                        ", ",
                        resultadoCreacion.Errors
                    );

                    throw new InvalidOperationException(
                        "No fue posible crear el usuario administrador: "
                        + errores
                    );
                }
            }

            if (!userManager.IsInRole(
                administrador.Id,
                "Administrador"))
            {
                var resultadoRol = userManager.AddToRole(
                    administrador.Id,
                    "Administrador"
                );

                if (!resultadoRol.Succeeded)
                {
                    string errores = string.Join(
                        ", ",
                        resultadoRol.Errors
                    );

                    throw new InvalidOperationException(
                        "No fue posible asignar el rol Administrador: "
                        + errores
                    );
                }
            }
        }

        private static void CrearViviendas(CondominioContext context)
        {
            context.Viviendas.AddOrUpdate(
                v => new { v.Numero, v.Bloque },
                new Vivienda
                {
                    Numero = "101",
                    Bloque = "A",
                    Tipo = TipoVivienda.Casa,
                    Estado = EstadoGeneral.Activo
                },
                new Vivienda
                {
                    Numero = "102",
                    Bloque = "A",
                    Tipo = TipoVivienda.Casa,
                    Estado = EstadoGeneral.Activo
                },
                new Vivienda
                {
                    Numero = "201",
                    Bloque = "B",
                    Tipo = TipoVivienda.Casa,
                    Estado = EstadoGeneral.Activo
                },
                new Vivienda
                {
                    Numero = "301",
                    Bloque = "C",
                    Tipo = TipoVivienda.Lote,
                    Estado = EstadoGeneral.Activo
                }
            );
        }

        private static void CrearNoticias(CondominioContext context)
        {
            context.Noticias.AddOrUpdate(
                n => n.Titulo,
                new Noticia
                {
                    Titulo = "Corte de energía programado para el sábado",
                    Contenido = "Se informa a los residentes que el día sábado se realizará un corte de energía de 8:00 a.m. a 12:00 p.m. para realizar labores de mantenimiento en la subestación eléctrica.",
                    FechaPublicacion = DateTime.Now.AddDays(-1),
                    EsAlerta = true,
                    Autor = "Administración"
                },
                new Noticia
                {
                    Titulo = "Recordatorio: pago de cuota de mantenimiento",
                    Contenido = "Les recordamos que el pago de la cuota de mantenimiento vence el último día del mes. Pueden realizar el pago en la oficina de administración en horario de 8:00 a.m. a 4:00 p.m.",
                    FechaPublicacion = DateTime.Now.AddDays(-2),
                    EsAlerta = true,
                    Autor = "Administración"
                },
                new Noticia
                {
                    Titulo = "Mejoras en el área de piscina",
                    Contenido = "Se instalaron nuevas lámparas y se realizó el mantenimiento general del área de piscina. Les recordamos las normas de uso para conservar las instalaciones en buen estado.",
                    FechaPublicacion = DateTime.Now.AddDays(-3),
                    EsAlerta = false,
                    Autor = "Administración"
                },
                new Noticia
                {
                    Titulo = "Nuevo horario de vigilancia",
                    Contenido = "Se informa que el servicio de vigilancia ahora estará disponible las 24 horas. Ante cualquier emergencia pueden contactar al guardia de turno por el intercomunicador de la entrada principal.",
                    FechaPublicacion = DateTime.Now.AddDays(-4),
                    EsAlerta = false,
                    Autor = "Administración"
                }
            );
        }
    }
}
