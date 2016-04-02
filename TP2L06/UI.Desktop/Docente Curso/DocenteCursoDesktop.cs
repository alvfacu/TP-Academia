using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Business.Entities;
using Negocio;
using Util;

namespace UI.Desktop
{
    public partial class DocenteCursoDesktop : ApplicationForm
    {
        #region Variables

        private DocenteCurso _docCurActual;

        #endregion

        #region Constructores

        public DocenteCursoDesktop()
        {
            InitializeComponent();
            this.Modo = ModoForm.Baja;
            InicializarComboBox();
        }

        public DocenteCursoDesktop(ModoForm modo)
            : this()
        {
            this.Modo = modo;
        }

        public DocenteCursoDesktop(int ID, ModoForm modo)
            : this()
        {
            this.Modo = modo;
            DocCurActual = new DocenteLogic().GetOne(ID);
            MapearDeDatos();
            switch (modo)
            {
                case (ModoForm.Alta):
                    {                        
                        this.btnAceptar.Text = "Guardar";
                        break;
                    }
                case (ModoForm.Modificacion):
                    {
                        this.btnAceptar.Text = "Guardar";
                        break;
                    }
                case (ModoForm.Baja):
                    {
                        this.btnAceptar.Text = "Eliminar";
                        break;
                    }
                case (ModoForm.Consulta):
                    {
                        this.btnAceptar.Text = "Aceptar";
                        break;
                    }
            }
        }

        #endregion

        #region Propiedades

        public DocenteCurso DocCurActual
        {
            get { return _docCurActual; }
            set { _docCurActual = value; }
        }

        #endregion

        #region Metodos

        private void InicializarComboBox()
        {
            cmbCargo.DataSource = new DocenteLogic().DevolverTiposCargos();

            cmbCurso.DataSource = new CursoLogic().GetAll();
            cmbCurso.DisplayMember = "descripcion";

            cmbDocente.DataSource = new PersonaLogic().DevolverDocentes();
            cmbDocente.DisplayMember = "nombre";
        }

        public virtual void MapearDeDatos() 
        {
            this.txtID.Text = this.DocCurActual.ID.ToString();
            this.cmbDocente.SelectedIndex = new PersonaLogic().DameIndex(this.DocCurActual.IDDocente, Business.Entities.Personas.TiposPersonas.Docente);
            this.cmbCurso.SelectedIndex = new CursoLogic().DameIndex(this.DocCurActual.IDCurso);
            this.cmbCargo.SelectedIndex = new DocenteLogic().DameIndexCargo(this.DocCurActual.Cargo);
        }

        public virtual void GuardarCambios()
        {
            try
            {
                MapearADatos();
                new DocenteLogic().Save(DocCurActual);
            }
            catch (ErrorEliminar ex)
            {
                Notificar("Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public virtual void MapearADatos() 
        {
            Business.Entities.Curso cursoActual = this.DevolverCurso();
            Business.Entities.Personas docenteActual = this.DevolverPersona();
            switch (this.Modo)
            {
                case (ModoForm.Alta):
                    {
                        DocCurActual = new DocenteCurso();
                        this.DocCurActual.IDCurso = cursoActual.ID;
                        this.DocCurActual.IDDocente = docenteActual.ID;
                        this.DocCurActual.Cargo = (DocenteCurso.TiposCargos)Enum.Parse(typeof(DocenteCurso.TiposCargos),this.cmbCargo.Text);
                        this.DocCurActual.State = BusinessEntity.States.New;
                        break; 
                    }
                case (ModoForm.Modificacion):
                    {
                        this.DocCurActual.IDCurso = cursoActual.ID;
                        this.DocCurActual.IDDocente = docenteActual.ID;
                        this.DocCurActual.Cargo = (DocenteCurso.TiposCargos)Enum.Parse(typeof(DocenteCurso.TiposCargos), this.cmbCargo.Text);
                        this.DocCurActual.State = BusinessEntity.States.Modified;
                        break;
                    }
                case (ModoForm.Baja):
                    {
                        this.DocCurActual.State = BusinessEntity.States.Deleted;
                        break;
                    }
                case (ModoForm.Consulta):
                    {
                        this.DocCurActual.State = BusinessEntity.States.Unmodified;
                        break;
                    }
            }
        }

        private Business.Entities.Personas DevolverPersona()
        {
            return new PersonaLogic().GetOne(((Business.Entities.Personas)this.cmbDocente.SelectedValue).ID);
        }

        private Curso DevolverCurso()
        {
            return new CursoLogic().GetOne(((Business.Entities.Curso)this.cmbCurso.SelectedValue).ID);
        }               
        
        public virtual bool Validar() 
        {
            return true;
        }
        
        private bool EstaInscripto()
        {
            bool estado = new DocenteLogic().DocenteEstaInscripto(((Business.Entities.Personas)this.cmbDocente.SelectedValue).ID, ((Business.Entities.Curso)this.cmbCurso.SelectedValue).ID);
            if (estado)
            {
                Notificar("Dictado repetido", "El Docente ya se encuentra registrado en ese Curso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return estado;
        }

        #endregion

        #region Eventos

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                if ((this.Modo != ModoForm.Alta) || !(EstaInscripto()))
                {
                    GuardarCambios();
                    Close();
                }
            }            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmbDocente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(this.Modo == ModoForm.Baja || this.Modo == ModoForm.Modificacion))
            {

                if (Login.UsuarioLogueado.Per.TipoPersona == Business.Entities.Personas.TiposPersonas.Administrador)
                {
                    cmbCurso.DataSource = new CursoLogic().GetAllXPlan(new PersonaLogic().DameIDPlanPersona(cmbDocente.SelectedIndex, Business.Entities.Personas.TiposPersonas.Docente));
                }
                else
                {
                    cmbCurso.DataSource = new CursoLogic().GetAllXPlan(Login.UsuarioLogueado.Per.IDPlan);
                }
                cmbCurso.DisplayMember = "descripcion";
            }
        }

        #endregion

    }
}
