﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Entities
{
    public class Modulo: BusinessEntity
    {
        string _Descripcion, _Ejecuta;

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public String Ejecuta
        {
            get { return _Ejecuta; }
            set { _Ejecuta = value; }
        }


    }
}
