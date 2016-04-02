using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Entities;
using Data.Database;

namespace Negocio
{
    public class PersonaLogic: BusinessLogic
    {
        #region Variable

        private Data.Database.PersonaAdapter _PersonaData;

        #endregion

        #region Constructor

        public PersonaLogic()
        {
            PersonaData = new Data.Database.PersonaAdapter();
        }

        #endregion

        #region Propiedad

        public Data.Database.PersonaAdapter PersonaData
        {
            get { return _PersonaData; }
            set { _PersonaData = value; }
        }

        #endregion

        #region Metodos

        public List<Personas> GetAll()
        {
            return PersonaData.GetAll();
        }

        public Business.Entities.Personas GetOne(int ID)
        {
            return PersonaData.GetOne(ID);
        }

        public void Delete(int ID)
        {
            PersonaData.Delete(ID);
        }

        public void Save(Business.Entities.Personas per)
        {
            PersonaData.Save(per);
        }
        
        public List<Personas> DevolverAlumnos()
        {
            return PersonaData.GetAllAlumnos();
        }

        public List<Personas> DevolverDocentes()
        {
            return PersonaData.GetAllDocentes();
        }

        public List<String> DevolverTiposPersonas()
        {
            return Business.Entities.Personas.DevolverTiposPersonas();
        }
        
        public int DameIndex(Personas perActual)
        {
            List<Personas> p = PersonaData.GetAll();
            int i = 0;
            foreach(Business.Entities.Personas per in p)
            {
                if (per.ID == perActual.ID)
                {
                    break;
                }
                ++i;
            }
            return i;
        }

        public int DameIndexTipo(Personas.TiposPersonas tipo)
        {
            int i = 0;
            List<string> tipos = Personas.DevolverTiposPersonas();
            foreach(String t in tipos)
            {
                if (tipo.ToString().Equals(t))
                {
                    break;
                }
                ++i;
            }
            return i;
        }
        
        public int DameIndex(int id, Personas.TiposPersonas tipo)
        {
            int i = 0;
            List<Personas> personas;
            if (tipo == Personas.TiposPersonas.Alumno)
            {
                personas = PersonaData.GetAllAlumnos();
            }
            else
            {
                personas = PersonaData.GetAllDocentes();
            }
            foreach (Personas p in personas)
            {
                if (p.ID == id)
                {
                    break;
                }
                ++i;
            }
            return i;
        }

        public Personas DamePersona(int n)
        {
            return PersonaData.GetAll()[n];
        }

        public int DameIDPlanPersona(int ind, Business.Entities.Personas.TiposPersonas tipo)
        {
            List<Personas> personas = new List<Personas>();
            if (tipo == Personas.TiposPersonas.Alumno)
            {
                personas = PersonaData.GetAllAlumnos();
            }
            else
            {
                personas = PersonaData.GetAllDocentes();
            }
            Personas per = PersonaData.GetOne(personas[ind].ID);
            return per.IDPlan;
        }


        public int DameIDPersona(int p, Personas.TiposPersonas tipo)
        {
            List<Personas> personas = new List<Personas>();
            if (tipo == Personas.TiposPersonas.Alumno)
            {
                personas = PersonaData.GetAllAlumnos();
            }
            else
            {
                personas = PersonaData.GetAllDocentes();
            }
            return personas[p].ID;
        }

        public bool LegajoRepetido(int leg)
        {
            if (PersonaData.GetAll().Count(p => p.Legajo == leg) != 0)
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
