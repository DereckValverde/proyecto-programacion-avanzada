using System;

namespace proyecto_programacion_avanzada.ViewModels.AreaComun
{
    public class AreaComunListViewModel
    {
        public int IdArea { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int Capacidad { get; set; }

        public TimeSpan HoraApertura { get; set; }

        public TimeSpan HoraCierre { get; set; }
    }
}