using System;
using System.ComponentModel.DataAnnotations;

namespace proyecto_programacion_avanzada.ViewModels.AreaComun
{
    public class AreaComunEditViewModel
    {
        public int IdArea { get; set; }

        [Required(ErrorMessage = "El nombre del área común es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        [Display(Name = "Nombre del área común")]
        public string Nombre { get; set; }

        [StringLength(250, ErrorMessage = "La descripción no puede superar los 250 caracteres.")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La capacidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La capacidad debe ser mayor que cero.")]
        [Display(Name = "Capacidad máxima")]
        public int Capacidad { get; set; }

        [Required(ErrorMessage = "La hora de apertura es obligatoria.")]
        [Display(Name = "Hora de apertura")]
        [DataType(DataType.Time)]
        public TimeSpan HoraApertura { get; set; }

        [Required(ErrorMessage = "La hora de cierre es obligatoria.")]
        [Display(Name = "Hora de cierre")]
        [DataType(DataType.Time)]
        public TimeSpan HoraCierre { get; set; }
    }
}