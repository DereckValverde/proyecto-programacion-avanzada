using System;
using System.ComponentModel.DataAnnotations;

namespace proyecto_programacion_avanzada.ViewModels.AreaComun
{
    public class AreaComunDetailsViewModel
    {
        public int IdArea { get; set; }

        [Display(Name = "Nombre del área común")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Capacidad máxima")]
        public int Capacidad { get; set; }

        [Display(Name = "Hora de apertura")]
        [DataType(DataType.Time)]
        public TimeSpan HoraApertura { get; set; }

        [Display(Name = "Hora de cierre")]
        [DataType(DataType.Time)]
        public TimeSpan HoraCierre { get; set; }
    }
}