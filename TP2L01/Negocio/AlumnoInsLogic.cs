using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Entities;
using Data.Database;

namespace Negocio
{
    public class AlumnoInsLogic: BusinessLogic
    {
        #region Variables

        private AlumnoInsAdapter _AlumnoData;

        #endregion

        #region Constructores

        public AlumnoInsLogic()
        {
            AlumnoData = new AlumnoInsAdapter();
        }

        #endregion

        #region Propiedades

        public AlumnoInsAdapter AlumnoData
        {
            get { return _AlumnoData; }
            set { _AlumnoData = value; }
        }

        #endregion

        #region Metodos

        public List<AlumnoInscripcion> GetAll()
        {
            return AlumnoData.GetAll();
        }

        public AlumnoInscripcion GetOne(int ID)
        {
            return AlumnoData.GetOne(ID);
        }

        public void Delete(int ID)
        {
            AlumnoData.Delete(ID);
        }

        public void Save(AlumnoInscripcion alu)
        {            
            AlumnoData.Save(alu);
        }

        //WEB
        public bool HayCupo(string desccurso)
        {
            Curso cur = new CursoLogic().DameCurso(desccurso);
            int inscripciones = AlumnoData.DevolverCantidadInscripciones(cur.ID);
            if (inscripciones < cur.Cupo)
            {
                return true;
            }
            else
            {
                return false;
            }            
        }

        //ESCRITORIO
        public bool HayCupo(int id)
        {
            Curso cur = new CursoLogic().GetOne(id);
            int inscripciones = AlumnoData.DevolverCantidadInscripciones(id);
            if (inscripciones < cur.Cupo)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
                
        public bool AlumnoEstaInscripto(int idAlumno, int idCurso)
        {
            if (AlumnoData.GetAll().Count(c => c.IDAlumno == idAlumno && c.IDCurso == idCurso) != 0)
            {
                return true;
            }
            else
            {                
                return false;
            }
        }

        public object GetAllXAlumno(int id)
        {
            return AlumnoData.GetAllXAlumno(id);
        }
        
        public object GetAllAlumnosXDocente(int id)
        {
            return AlumnoData.GetAllXDocente(id);
        }

        #endregion
    }
}
