using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Entities;
using Data.Database;

namespace Negocio
{
    public class ModuloUsuarioLogic: BusinessLogic
    {
        #region Variable

        private ModuloUsuarioAdapter _ModUsuarioData;

        #endregion

        #region Constructor

        public ModuloUsuarioLogic()
        {
            ModUsuarioData = new ModuloUsuarioAdapter();
        }

        #endregion

        #region Propiedad

        public ModuloUsuarioAdapter ModUsuarioData
        {
            get { return _ModUsuarioData; }
            set { _ModUsuarioData = value; }
        }

        #endregion

        #region Metodos

        public List<ModuloUsuario> GetAll()
        {
            return ModUsuarioData.GetAll();
        }

        public List<ModuloUsuario> GetAll(int idUsuario)
        {
            return ModUsuarioData.GetAll(idUsuario);
        }

        public ModuloUsuario GetOne(int ID)
        {
            return ModUsuarioData.GetOne(ID);
        }

        public ModuloUsuario GetOne(int idModulo, int idUsuario)
        {
            return ModUsuarioData.GetOne(idModulo, idUsuario);
        }

        public void Delete(int ID)
        {
            ModUsuarioData.Delete(ID);
        }

        public void Save(ModuloUsuario mod)
        {
            ModUsuarioData.Save(mod);
        }

        #endregion
    }
}
