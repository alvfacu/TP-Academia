using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Entities;
using Data.Database;

namespace Negocio
{
    public class EspecialidadLogic: BusinessLogic
    {
        #region Variable

        private Data.Database.EspecialidadAdapter _EspecialidadData;

        #endregion

        #region Constructor

        public EspecialidadLogic()
        {
            EspecialidadData = new Data.Database.EspecialidadAdapter();
        }

        #endregion

        #region Propiedad

        public Data.Database.EspecialidadAdapter EspecialidadData
        {
            get { return _EspecialidadData; }
            set { _EspecialidadData = value; }
        }

        #endregion

        #region Metodos

        public List<Especialidad> GetAll()
        {
            return EspecialidadData.GetAll();
        }

        public Business.Entities.Especialidad GetOne(int ID)
        {
            return EspecialidadData.GetOne(ID);
        }

        public void Delete(int ID)
        {
            EspecialidadData.Delete(ID);
        }

        public void Save(Business.Entities.Especialidad esp)
        {
            EspecialidadData.Save(esp);
        }

        public int DameIndex(int id)
        {
            List<Especialidad> esp = EspecialidadData.GetAll();
            int i = 0;
            foreach (Business.Entities.Especialidad especialidad in esp)
            {
                if (especialidad.ID == id)
                {
                    break;
                }
                ++i;
            }
            return i;
        }

        public int DameIDEspecialidad(int ind)
        {
            return EspecialidadData.GetAll()[ind].ID;
        }

        #endregion

    }
}
