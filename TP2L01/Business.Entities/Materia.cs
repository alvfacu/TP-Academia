using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Entities
{
    public class Materia: BusinessEntity
    {
        private string _Descripcion, _Desc_Plan;
        private int _HSSemanales, _HSTotales, _IDPlan;

        public string Descripcion 
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public int HSSemanales
        {
            get { return _HSSemanales; }
            set { _HSSemanales = value; }
        }

        public int HSTotales
        {
            get { return _HSTotales; }
            set { _HSTotales = value; }
        }

        public int IDPlan
        {
            get { return _IDPlan; }
            set { _IDPlan = value; }
        }

        public string Desc_Plan
        {
            get { return _Desc_Plan; }
            set { _Desc_Plan = value; }
        }
    }
}
