using System.ComponentModel.DataAnnotations;

namespace Condominio.Application.Login
{
    public class LoginViewModel
    {
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Contrasena { get; set; }
    }
}
