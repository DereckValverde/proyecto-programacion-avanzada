using Microsoft.AspNet.Identity.EntityFramework;

namespace Condominio.Domain.Identity
{
    public class ApplicationUserLogin : IdentityUserLogin<int>
    {
    }

    public class ApplicationUserRole : IdentityUserRole<int>
    {
    }

    public class ApplicationUserClaim : IdentityUserClaim<int>
    {
    }

    public class ApplicationRole : IdentityRole<int, ApplicationUserRole>
    {
    }
}
