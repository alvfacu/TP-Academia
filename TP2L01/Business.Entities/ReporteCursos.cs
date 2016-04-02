using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Entities
{
    public class ReporteCursos
    {
        public String comision, materia, plan, especialidad;
        public int hsSemanales, hsTotales, año;
        public int Año { get { return año; } set { año = value; } }
        public int HsSemanales { get { return hsSemanales; } set { hsSemanales = value; } }
        public int HsTotales { get { return hsTotales; } set { hsTotales = value; } }
        public String Comision { get { return comision; } set { comision = value; } }
        public String Materia { get { return materia; } set { materia = value; } }
        public String Plan { get { return plan; } set { plan = value; } }
        public String Especialidad { get { return especialidad; } set { especialidad = value; } }
    }
}
