using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using proyecto_programacion_avanzada.Common.Enums;

namespace proyecto_programacion_avanzada.Entities
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(150)]
        [Index("IX_Usuario_Correo", IsUnique = true)]
        public string Correo { get; set; }

        [StringLength(20)]
        [Phone]
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