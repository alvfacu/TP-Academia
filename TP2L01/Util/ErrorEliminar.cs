using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util
{
    public class ErrorEliminar : Exception
    {
        public ErrorEliminar()
            : base("No se pudo eliminar") //Este es el mensaje por defecto, asignado a la clase base
        { }

        public ErrorEliminar(string Mensaje)
            : base(Mensaje)
        { }

        public ErrorEliminar(string Mensaje, Exception anidada)
            : base(Mensaje, anidada)
        { }
    }
}

