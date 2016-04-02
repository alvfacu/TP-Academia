using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Entities
{
    public class ModuloUsuario: BusinessEntity
    {
        int _IdUsuario, _IdModulo;
        bool _PermiteAlta, _PermiteBaja, _PermiteModificacion, _PermiteConsulta;
        string _Usuario, _Modulo;

        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }

        public int IdModulo
        {
            get { return _IdModulo; }
            set { _IdModulo = value; }
        }

        public bool PermiteAlta
        {
            get { return _PermiteAlta; }
            set { _PermiteAlta = value; }
        }

        public bool PermiteBaja
        {
            get { return _PermiteBaja; }
            set { _PermiteBaja = value; }
        }

        public bool PermiteModificacion
        {
            get { return _PermiteModificacion; }
            set { _PermiteModificacion = value; }
        }

        public bool PermiteConsulta
        {
            get { return _PermiteConsulta; }
            set { _PermiteConsulta = value; }
        }

        public string Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }

        public string Modulo
        {
            get { return _Modulo; }
            set { _Modulo = value; }
        }
    }
}
