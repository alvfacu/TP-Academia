using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Entities;
using Data.Database;

namespace Negocio
{
    public class MateriaLogic: BusinessLogic
    {
        #region Variable

        private MateriaAdapter _MateriaData;

        #endregion

        #region Constructor

        public MateriaLogic()
        {
            MateriaData = new MateriaAdapter();
        }

        #endregion

        #region Propiedad

        public MateriaAdapter MateriaData
        {
            get { return _MateriaData; }
            set { _MateriaData = value; }
        }

        #endregion

        #region Metodos

        public List<Materia> GetAll()
        {
            return MateriaData.GetAll();
        }

        public Materia GetOne(int ID)
        {
            return MateriaData.GetOne(ID);
        }

        public void Delete(int ID)
        {
            MateriaData.Delete(ID);
        }

        public void Save(Materia mat)
        {
            MateriaData.Save(mat);
        }
        
        public int DameIndex(int ind)
        {
            List<Materia> materias = MateriaData.GetAll();
            int i = 0;
            foreach (Materia mat in materias)
            {
                if (mat.ID == ind)
                {
                    break;
                }
                ++i;
            }
            return i;
        }

        public int DameIDMateria(int p)
        {
            return MateriaData.GetAll()[p].ID;
        }

        public int DameIDPlanMateria(int p)
        {
            return MateriaData.GetAll()[p].IDPlan;
        }
        
        public object GetAllXPlan(int p)
        {
            return MateriaData.GetAllXPlan(p);
        }

        #endregion
    }
}
