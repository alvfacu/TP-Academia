using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Business.Entities;
using Util;

namespace UI.Web
{
    public partial class DocentesCursos : Base
    {
        #region Variables

        DocenteLogic _logic;

        #endregion

        #region Propiedades

        private DocenteLogic Logic
        {
            get
            {
                if (_logic == null)
                {
                    _logic = new DocenteLogic();
                }
                return _logic;
            }
        }
        
        private DocenteCurso Entity
        {
            get;
            set;
        }
        
        #endregion

        #region Metodos

        private void LoadGrid()
        {
            if (((Usuario)Session["logueo"]).Per.TipoPersona == Business.Entities.Personas.TiposPersonas.Docente)
            {
                this.gridView.DataSource = this.Logic.GetAllXDocente(((Usuario)Session["logueo"]).Per.ID);
            }
            else if (((Usuario)Session["logueo"]).Per.TipoPersona == Business.Entities.Personas.TiposPersonas.Alumno)
            {
                this.gridView.DataSource = this.Logic.GetAllXPlan(((Usuario)Session["logueo"]).Per.IDPlan);
            }
            else
            {
                this.gridView.DataSource = this.Logic.GetAll();
            }
            this.gridView.DataBind();
        }

        private void LoadForm(int id)
        {
            this.Entity = this.Logic.GetOne(id);
            this.docentesList.SelectedIndex = new PersonaLogic().DameIndex(this.Entity.IDDocente, Business.Entities.Personas.TiposPersonas.Docente);
            this.cargosList.SelectedIndex = new DocenteLogic().DameIndexCargo(this.Entity.Cargo);
            CargarCursos();
            this.cursoList.SelectedIndex = new CursoLogic().DameIndex(this.Entity.IDCurso);            
        }

        private void EnableForm(bool enable)
        {
            this.cursoList.Enabled = enable;
            this.docentesList.Enabled = enable;
            this.cargosList.Enabled = enable;
        }

        private void DeleteEntity(int id)
        {
            try
            {
                this.Logic.Delete(id);
            }
            catch (ErrorEliminar ex)
            {
                this.errorPanel.Visible = true;
                this.usuarioPanel.Visible = false;
                this.mensajeError.Text = ex.Message;
                this.aceptarLinkButton.Enabled = false;
            }
        }

        private void CargarListas()
        {
            this.docentesList.DataSource = new PersonaLogic().DevolverDocentes();
            this.cargosList.DataSource = new DocenteLogic().DevolverTiposCargos();
            
            this.docentesList.DataTextField = "nombre";

            this.docentesList.DataBind();
            this.cargosList.DataBind();
        }
        
        private void LoadEntity(DocenteCurso docurso)
        {
            docurso.IDDocente = new PersonaLogic().DameIDPersona(this.docentesList.SelectedIndex, Business.Entities.Personas.TiposPersonas.Docente);
            if (this.FormMode == FormModes.Alta)
            {
                docurso.IDCurso = new CursoLogic().DameIDCurso(this.cursoList.SelectedIndex, docurso.IDDocente);
            }
            else
            {
                docurso.IDCurso = new CursoLogic().DameIDCurso(this.cursoList.SelectedIndex);
            }                 
            docurso.Cargo = (DocenteCurso.TiposCargos)Enum.Parse(typeof(DocenteCurso.TiposCargos), this.cargosList.SelectedValue);
        }
        
        private void SaveEntity(DocenteCurso docurso)
        {
            this.Logic.Save(docurso);
        }

        private void CargarCursos()
        {
            if (((Usuario)Session["logueo"]).Per.TipoPersona == Business.Entities.Personas.TiposPersonas.Administrador && this.FormMode == FormModes.Alta)
            {
                this.cursoList.DataSource = new CursoLogic().GetAllXPlan(new PersonaLogic().DameIDPlanPersona(this.docentesList.SelectedIndex, Business.Entities.Personas.TiposPersonas.Docente));

            }
            else if (this.FormMode == FormModes.Baja || this.FormMode == FormModes.Modificacion)
            {
                this.cursoList.DataSource = new CursoLogic().GetAll();
            }
            else
            {
                this.cursoList.DataSource = new CursoLogic().GetAllXPlan(((Usuario)Session["logueo"]).Per.IDPlan);
            }

            this.cursoList.DataTextField = "descripcion";
            this.cursoList.DataBind();
        }
        
        private bool EstaInscripto()
        {
            int idDoc = new PersonaLogic().DameIDPersona(this.docentesList.SelectedIndex, Business.Entities.Personas.TiposPersonas.Docente);

            return new DocenteLogic().DocenteEstaInscripto(idDoc, new CursoLogic().DameIDCurso(this.cursoList.SelectedIndex, idDoc));
        }
        
        private void ClearForm()
        {
            this.cargosList.SelectedIndex = 0;
            this.cursoList.SelectedIndex = -1;
            this.docentesList.SelectedIndex = 0;
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            DeshabilitarOpciones();
            if (((Usuario)Session["logueo"]).Per.TipoPersona != Business.Entities.Personas.TiposPersonas.Administrador)
            {
                if (!Page.IsPostBack)
                {
                    LoadGrid();
                    this.adminPanel.Visible = true;
                    this.usuarioPanel.Visible = false;
                    this.gridPanel.Visible = true;
                    if (((Usuario)Session["logueo"]).Per.TipoPersona != Business.Entities.Personas.TiposPersonas.Administrador)
                    {
                        this.gridActionPanel.FindControl("nuevoLinkButton").Visible = false;
                        this.gridActionPanel.FindControl("editarLinkButton").Visible = false;
                        this.gridActionPanel.FindControl("eliminarLinkButton").Visible = false;
                    }
                    else
                    {
                        this.gridActionPanel.Visible = false;
                    }
                    this.gridView.Columns[3].Visible = false;
                }
                else
                {
                    this.errorPanel.Visible = false;
                    this.aceptarLinkButton.Enabled = true;
                    this.usuarioPanel.Visible = true;
                }
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    this.FormMode = FormModes.Baja;
                    this.editarLinkButton.Visible = false;
                    this.eliminarLinkButton.Visible = false;
                    LoadGrid();
                    CargarListas();
                    CargarCursos();
                }
                else
                {
                    LoadGrid();
                    this.errorPanel.Visible = false;
                    this.aceptarLinkButton.Enabled = true;
                }
            }          
        }

        protected void gridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.editarLinkButton.Visible = true;
            this.eliminarLinkButton.Visible = true;
            this.SelectedID = (int)this.gridView.SelectedValue;
            this.usuarioPanel.Visible = false;
        }

        protected void editarLinkButton_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                this.EnableForm(true);
                this.usuarioPanel.Visible = true;
                this.FormMode = FormModes.Modificacion;
                this.LoadForm(this.SelectedID);
            }
        }

        protected void aceptarLinkButton_Click(object sender, EventArgs e)
        {
            switch (this.FormMode)
            {
                case (FormModes.Modificacion):
                    {
                        this.Entity = new DocenteCurso();
                        this.Entity.ID = this.SelectedID;
                        this.Entity.State = BusinessEntity.States.Modified;
                        this.LoadEntity(this.Entity);
                        this.SaveEntity(this.Entity);
                        this.LoadGrid();
                        break;
                    }
                case (FormModes.Baja):
                    {
                        this.DeleteEntity(this.SelectedID);
                        this.LoadGrid();
                        break;
                    }
                case (FormModes.Alta):
                    {
                        if (!EstaInscripto())
                        {
                            this.Entity = new DocenteCurso();
                            this.LoadEntity(this.Entity);
                            this.SaveEntity(this.Entity);
                            this.LoadGrid();
                        }
                        else
                        {
                            this.errorPanel.Visible = true;
                            this.mensajeError.Text = "Dictado repetido: Docente ya se encuentra registrado a dicho Curso.";
                        }
                        break;
                    }
                default:
                    break;
            }
            if (!(this.errorPanel.Visible))
            {
                Response.Redirect("DocentesCursos.aspx");
            }
        }
        
        protected void eliminarLinkButton_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                this.usuarioPanel.Visible = true;
                this.EnableForm(false);
                this.aceptarLinkButton.CausesValidation = false;
                this.LoadForm(this.SelectedID);
                this.FormMode = FormModes.Baja;
            }
        }

        protected void nuevoLinkButton_Click(object sender, EventArgs e)
        {
            this.usuarioPanel.Visible = true;
            this.FormMode = FormModes.Alta;
            this.ClearForm();
            this.EnableForm(true);
        }


        protected void cancelarLinkButton_Click(object sender, EventArgs e)
        {
            if (((Usuario)Session["logueo"]).Per.TipoPersona == Business.Entities.Personas.TiposPersonas.Administrador)
            {
                //this.usuarioPanel.Visible = false;
                Response.Redirect("DocentesCursos.aspx");
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void docentesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.FormMode != FormModes.Baja || this.FormMode != FormModes.Modificacion)
            {
                CargarCursos();
            }          
        }

        #endregion        
    }
}