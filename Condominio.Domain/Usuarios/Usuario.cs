using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Condominio.Domain.Identity;
using Condominio.Domain.Shared;

namespace Condominio.Domain.Usuarios
{
    [Table("AspNetUsers")]
    public class Usuario : IdentityUser<int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(20)]
        [Phone]
        public string Telefono { get; set; }

        [Required]
        public RolUsuario Rol { get; set; }

        [Required]
        public EstadoGeneral Estado { get; set; }
    }
}
