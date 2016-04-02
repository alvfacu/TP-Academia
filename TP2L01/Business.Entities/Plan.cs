using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Entities
{
    public class Plan: BusinessEntity
    {
        private string _Descripcion, _Especialidad;
        private int _IDEspececialidad;

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public int IDEspecialidad
        {
            get { return _IDEspececialidad; }
            set { _IDEspececialidad = value; }
        }

        public string Especialidad
        {
            get { return _Especialidad; }
            set { _Especialidad = value; }
        }

    }
}
