using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Entities;
using Data.Database;

namespace Negocio
{
    public class ModuloLogic : BusinessLogic
    {
        #region Variable

        private ModuloAdapter _ModuloData;

        #endregion

        #region Constructor

        public ModuloLogic()
        {
            ModuloData = new ModuloAdapter();
        }

        #endregion

        #region Propiedad

        public ModuloAdapter ModuloData
        {
            get { return _ModuloData; }
            set { _ModuloData = value; }
        }

        #endregion

        #region Metodos

        public List<Modulo> GetAll()
        {
            return ModuloData.GetAll();
        }

        public Modulo GetOne(int ID)
        {
            return ModuloData.GetOne(ID);
        }

        public void Delete(int ID)
        {
            ModuloData.Delete(ID);
        }

        public void Save(Modulo modu)
        {
            ModuloData.Save(modu);
        }

        public int DameIndex(int idModulo)
        {
            List<Modulo> modulos = ModuloData.GetAll();
            int i = 0;
            foreach (Modulo mod in modulos)
            {
                if (mod.ID == idModulo)
                {
                    break;
                }
                ++i;
            }
            return i;
        }

        public int DameIDModulo(int ind)
        {
            return ModuloData.GetAll()[ind].ID;
        }

        #endregion
    }
}
