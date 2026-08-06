using System.ComponentModel.DataAnnotations;

namespace proyecto_programacion_avanzada.ViewModels.Login
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
