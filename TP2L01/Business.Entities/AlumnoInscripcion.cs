using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Entities
{
    public class AlumnoInscripcion: BusinessEntity
    {
        #region Variables

        private string _Condicion, _Nombre, _Descripcion;
        private int _IDAlumno, _IDCurso, _Nota;

        #endregion

        #region Propiedades

        public string Condicion
        {
            get { return _Condicion; }
            set { _Condicion = value.ToUpper(); }
        }

        public int IDAlumno
        {
            get { return _IDAlumno; }
            set { _IDAlumno = value; }
        }

        public int IDCurso
        {
            get { return _IDCurso; }
            set { _IDCurso = value; }
        }

        public int Nota
        {
            get { return _Nota; }
            set { _Nota = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        #endregion
    }
}
