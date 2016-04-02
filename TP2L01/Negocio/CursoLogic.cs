using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Entities;
using Data.Database;
using Business.Entities;

namespace Negocio
{
    public class CursoLogic : BusinessLogic
    {
        #region Variables

        private Data.Database.CursoAdapter _CursoData;

        #endregion

        #region Constructor

        public CursoLogic()
        {
            CursoData = new Data.Database.CursoAdapter();
        }

        #endregion

        #region Propiedades

        public Data.Database.CursoAdapter CursoData
        {
            get { return _CursoData; }
            set { _CursoData = value; }
        }

        public List<ReporteCursos> cursos
        {
            get { return reportesCurso(); }
        }

        #endregion

        #region Metodos

        public List<Curso> GetAll()
        {
            return CursoData.GetAll();
        }

        public Business.Entities.Curso GetOne(int ID)
        {
            return CursoData.GetOne(ID);
        }

        public void Delete(int ID)
        {
            CursoData.Delete(ID);
        }

        public void Save(Business.Entities.Curso cur)
        {
            CursoData.Save(cur);
        }              
        
        public int DameIndex(int ind)
        {
            int i = 0;
            List<Curso> cursos = CursoData.GetAll();
            foreach (Curso c in cursos)
            {
                if (c.ID == ind)
                {
                    break;
                }
                ++i;
            }
            return i;
        }

        public int DameIDCurso(int ind, int id)
        {
            Personas per = new PersonaLogic().GetOne(id);
            List<Curso> cursos = CursoData.GetAllXPlan(per.IDPlan);
            return cursos[ind].ID;
        }

        public int DameIDCurso(int ind)
        {
            return CursoData.GetAll()[ind].ID;
        }

        internal Business.Entities.Curso DameCurso(string desc)
        {
            List<Curso> cursos = new CursoLogic().GetAll();
            Curso cur = new Curso() ;
            foreach (Curso c in cursos)
            {
                if (String.Compare(c.Descripcion, desc, true) == 0)
                {
                    cur = c;
                }
            }

            return cur;
        }

        public List<Curso> GetAllXPlan(int id)
        {
            return CursoData.GetAllXPlan(id);
        }

        public List<ReporteCursos> reportesCurso()
        {
            return CursoData.reporteCurso();
        }

        #endregion
    }
}
