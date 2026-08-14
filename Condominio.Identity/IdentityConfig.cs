using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Condominio.Domain.Identity;
using Condominio.Domain.Usuarios;
using Condominio.Infrastructure.DbContexts;

namespace Condominio.Identity
{
    public class ApplicationUserManager : UserManager<Usuario, int>
    {
        public ApplicationUserManager(IUserStore<Usuario, int> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(CondominioContext context)
        {
            var manager = new ApplicationUserManager(
                new UserStore<Usuario, ApplicationRole, int,
                    ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(context));

            manager.UserValidator = new UserValidator<Usuario, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };

            return manager;
        }

        public static void AsegurarRol(CondominioContext context, string nombreRol)
        {
            var roleManager = new RoleManager<ApplicationRole, int>(
                new RoleStore<ApplicationRole, int, ApplicationUserRole>(context));

            if (!roleManager.RoleExists(nombreRol))
            {
                roleManager.Create(new ApplicationRole
                {
                    Name = nombreRol
                });
            }
        }
    }
}
