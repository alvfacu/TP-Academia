using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Entities
{
    public class Personas: BusinessEntity
    {
        public enum TiposPersonas
        {
            Administrador,
            Docente,
            Alumno,
        }

        private string _Apellido, _Nombre, _Email, _Direccion, _Telefono, _Completo, _Desc_Plan;
        private DateTime _FechaNacimiento;
        private int _IDPlan, _Legajo, _ID;
        private TiposPersonas _TipoPersona;

        public static void MostrarTiposPersonas()
        {
            Console.WriteLine("Tipos de Personas:");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(" {0}",(((TiposPersonas)i).ToString()));
            }
        }

        public static List<String> DevolverTiposPersonas()
        {
            List<String> array = new List<String>();
            for (int i = 0; i < 3; i++)
            {
                array.Add(((TiposPersonas)i).ToString());
            }
            return array;
        }
        
        public Personas()
        {
            TipoPersona = TiposPersonas.Administrador;
        }

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Apellido
        {
            get { return _Apellido; }
            set { _Apellido = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public DateTime FechaNacimiento
        {
            get { return _FechaNacimiento; }
            set { _FechaNacimiento = value; }
        }

        public int IDPlan
        {
            get { return _IDPlan; }
            set { _IDPlan = value; }
        }

        public int Legajo
        {
            get { return _Legajo; }
            set { _Legajo = value; }
        }

        public string Direccion
        {
            get { return _Direccion; }
            set { _Direccion = value; }
        }

        public string Telefono
        {
            get { return _Telefono; }
            set { _Telefono = value; }
        }

        public TiposPersonas TipoPersona
        {
            get { return _TipoPersona; }
            set { _TipoPersona = value; }
        }

        public String Completo
        {
            get { return _Completo; }
            set { _Completo = value; }
        }

        public string Desc_Plan
        {
            get { return _Desc_Plan; }
            set { _Desc_Plan = value; }
        }

    }
}
