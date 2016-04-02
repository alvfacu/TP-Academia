using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Entities;
using Data.Database;
using Util;

namespace Negocio
{
    public class UsuarioLogic: BusinessLogic
    {
        #region Variable

        private Data.Database.UsuarioAdapter _UsuarioData;

        #endregion

        #region Constructor

        public UsuarioLogic()
        {
            UsuarioData = new Data.Database.UsuarioAdapter();
        }

        #endregion

        #region Propiedad

        public Data.Database.UsuarioAdapter UsuarioData
        {
            get { return _UsuarioData; }
            set { _UsuarioData = value; } 
        }

        #endregion

        #region Metodos

        public List<Usuario> GetAll()
        {
            return UsuarioData.GetAll();
        }

        public Business.Entities.Usuario GetOne(int ID)
        {
            return UsuarioData.GetOne(ID);
        }

        public void Delete(int ID)
        {
            UsuarioData.Delete(ID);
        }

        public void Save(Business.Entities.Usuario usuario)
        {
            UsuarioData.Save(usuario);
        }
        
        public Usuario GetOneByUsuario(string p)
        {
            return UsuarioData.GetOneByUsuario(p);
        }

        public Usuario ValidarUsuario(string nombre, string clave)
        {
            bool est = true;
            Usuario usuarioActual = UsuarioData.GetOneByUsuario(nombre);
            if (usuarioActual == null)
            {
                est = false;
            }
            else
            {
                if (usuarioActual.Clave.CompareTo(clave) == 0)
                {
                    est = true;
                }
                else
                {
                    est = false;
                }
            }
            if (est)
            {
                return usuarioActual;
            }
            else
            {
                return null; 
            }
        }
        
        public int DameIndex(int id)
        {
            List<Usuario> usuarios = UsuarioData.GetAll();
            int i = 0;
            foreach (Usuario usr in usuarios)
            {
                if (usr.ID == id)
                {
                    break;
                }
                ++i;
            }
            return i;
        }

        public int DameIDUsuario(int p)
        {
            return UsuarioData.GetAll()[p].ID;
        }

        #endregion
    }
}
