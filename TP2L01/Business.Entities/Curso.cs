using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Entities
{
    public class Curso: BusinessEntity
    {
        private int _AnioCalendario, _Cupo, _IDComision, _IDMateria;
        private string _Descripcion, _Desc_Materia, _Desc_Comision;

        public int AnioCalendario
        {
            get { return _AnioCalendario; }
            set { _AnioCalendario = value; }
        }

        public int Cupo
        {
            get { return _Cupo; }
            set { _Cupo = value; }
        }

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public int IDComision
        {
            get { return _IDComision; }
            set { _IDComision = value; }
        }

        public int IDMateria
        {
            get { return _IDMateria; }
            set { _IDMateria = value; }
        }

        public string Desc_Materia
        {
            get { return _Desc_Materia; }
            set { _Desc_Materia = value; }
        }

        public string Desc_Comision
        {
            get { return _Desc_Comision; }
            set { _Desc_Comision = value; }
        }
    }
}
