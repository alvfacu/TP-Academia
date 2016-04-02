using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Entities;
using Data.Database;

namespace Negocio
{
    public class PlanLogic: BusinessLogic
    {
        #region Variable

        private Data.Database.PlanAdapter _PlanData;

        #endregion

        #region Propiedades
               
        public Data.Database.PlanAdapter PlanData
        {
            get { return _PlanData; }
            set { _PlanData = value; }
        }

        public List<ReportePlanes> planes
        {
            get
            {
                return _PlanData.reportePlanes();
            }
        }

        #endregion

        #region Constructor

        public PlanLogic()
        {
            PlanData = new Data.Database.PlanAdapter();
        }

        #endregion

        #region Metodos

        public List<Plan> GetAll()
        {
            return PlanData.GetAll();
        }

        public Business.Entities.Plan GetOne(int ID)
        {
            return PlanData.GetOne(ID);
        }

        public void Delete(int ID)
        {
            PlanData.Delete(ID);
        }

        public void Save(Plan plan)
        {
            PlanData.Save(plan);
        }
                
        public int DameIndex(int id)
        {
            List<Business.Entities.Plan> planes = PlanData.GetAll();
            int i = 0;
            foreach (Plan pl in planes)
            {
                if (pl.ID == id)
                {
                    break;
                }
                ++i;
            }
            return i;
        }

        public int DamePlan(int ind)
        {
            return PlanData.GetAll()[ind].ID;
        }

        public int DameIDPlan(int p)
        {
            return PlanData.GetAll()[p].ID;
        }
        
        public int DameIndexEspecialidad(int p)
        {
            List<Plan> planes = PlanData.GetAll();
            int idEsp = 0;
            foreach (Plan pl in planes)
            {
                if (pl.ID == p)
                {
                    idEsp = pl.IDEspecialidad;
                    break;
                }
            }

            List<Especialidad> especialidades = new EspecialidadLogic().GetAll();
            int i = 0;
            foreach (Especialidad esp in especialidades)
            {
                if (esp.ID == idEsp)
                {
                    break;                 
                }
                ++i;
            }
            return i;
        }

        public List<ReportePlanes> ReportePlanes()
        {
            return planes;
        }
                
        #endregion
    }
}
