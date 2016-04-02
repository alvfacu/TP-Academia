using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Entities;
using Data.Database;

namespace Negocio
{
    public class DocenteLogic: BusinessLogic
    {
        #region Variables

        private Data.Database.DocenteAdapter _DocenteData;

        #endregion

        #region Constructor

        public DocenteLogic()
        {
            DocenteData = new Data.Database.DocenteAdapter();
        }

        #endregion

        #region Propiedad

        public Data.Database.DocenteAdapter DocenteData
        {
            get { return _DocenteData; }
            set { _DocenteData = value; }
        }

        #endregion

        #region Metodos

        public List<DocenteCurso> GetAll()
        {
            return DocenteData.GetAll();
        }

        public Business.Entities.DocenteCurso GetOne(int ID)
        {
            return DocenteData.GetOne(ID);
        }

        public void Delete(int ID)
        {
            DocenteData.Delete(ID);
        }

        public void Save(Business.Entities.DocenteCurso doc)
        {
            DocenteData.Save(doc);
        }
        
        public List<String> DevolverTiposCargos()
        {
            return DocenteCurso.DevolverTiposCargos();
        }

        public int DameIndexCargo(DocenteCurso.TiposCargos tipo)
        {
            List<String> cargos = DocenteCurso.DevolverTiposCargos();
            int i = 0;
            foreach (String c in cargos)
            {
                if (tipo.ToString().Equals(c))
                {
                    break;
                }
                ++i;
            }
            return i;
        }

        public object GetAllXDocente(int id)
        {
            return DocenteData.GetAllXDocente(id);
        }

        public object GetAllXPlan(int p)
        {
            return DocenteData.GetAllXPlan(p);
        }

        public bool DocenteEstaInscripto(int idDocente, int idCurso)
        {
            if (DocenteData.GetAll().Count(c => c.IDDocente == idDocente && c.IDCurso == idCurso) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
