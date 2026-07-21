using System.ComponentModel.DataAnnotations;
using proyecto_programacion_avanzada.Common.Enums;

namespace proyecto_programacion_avanzada.DTOs
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Correo { get; set; }

        [Phone]
        [StringLength(20)]
        public string Telefono { get; set; }

        [Required]
        [StringLength(255)]
        public string Contrasena { get; set; }

        [Required]
        public RolUsuario Rol { get; set; }

        [Required]
        public EstadoGeneral Estado { get; set; }
    }
}