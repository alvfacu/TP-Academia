using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Entities;
using Data.Database;

namespace Negocio
{
    public class ComisionLogic: BusinessLogic
    {
        #region Variables

        private ComisionAdapter _ComisionData;

        #endregion

        #region Constructores

        public ComisionLogic()
        {
            ComisionData = new ComisionAdapter();
        }

        #endregion

        #region Propiedades

        public ComisionAdapter ComisionData
        {
            get { return _ComisionData; }
            set { _ComisionData = value; }
        }

        #endregion

        #region Metodos

        public List<Comision> GetAll()
        {
            return ComisionData.GetAll();
        }

        public Comision GetOne(int ID)
        {
            return ComisionData.GetOne(ID);
        }

        public void Delete(int ID)
        {
            ComisionData.Delete(ID);
        }

        public void Save(Comision com)
        {
            ComisionData.Save(com);
        }
        
        public int DameIndex(int ind)
        {
            List<Comision> comisiones = ComisionData.GetAll();
            int i = 0;
            foreach (Comision c in comisiones)
            {
                if (c.ID == ind)
                {
                    break;
                }
                ++i;
            }
            return i;
        }

        public int DameIDComision(int ind)
        {
            return ComisionData.GetAll()[ind].ID;
        }

        public List<Comision> GetAllXPlan(int plan)
        {
            return ComisionData.GetAllXPlan(plan);
        }

        public int DameIDComision(int ind, int mat)
        {
            return ComisionData.GetAllXPlan(new MateriaLogic().GetOne(mat).IDPlan)[ind].ID;
        }

        #endregion
    }
}
