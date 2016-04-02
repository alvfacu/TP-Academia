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
    public partial class Cursos : Base
    {
        #region Variables

        CursoLogic _logic;

        #endregion

        #region Propiedades

        private CursoLogic Logic
        {
            get
            {
                if (_logic == null)
                {
                    _logic = new CursoLogic();
                }
                return _logic;
            }
        }

        private Curso Entity
        {
            get;
            set;
        }
               
        #endregion

        #region Metodos

        private void LoadGrid()
        {
            if (((Usuario)Session["logueo"]).Per.TipoPersona != Business.Entities.Personas.TiposPersonas.Administrador)
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
            this.descripcionTextBox.Text = this.Entity.Descripcion;
            this.materiasList.SelectedIndex = new MateriaLogic().DameIndex(this.Entity.IDMateria);
            CargarComisiones();
            this.comisionesList.SelectedIndex = new ComisionLogic().DameIndex(this.Entity.IDComision);
            this.anioTextBox.Text = this.Entity.AnioCalendario.ToString();
            this.cupoTextBox.Text = this.Entity.Cupo.ToString();
        }

        private void EnableForm(bool enable)
        {
            this.descripcionTextBox.Enabled = enable;
            this.comisionesList.Enabled = enable;
            this.materiasList.Enabled = enable;
            this.anioTextBox.Enabled = enable;
            this.cupoTextBox.Enabled = enable;
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
            this.materiasList.DataSource = new MateriaLogic().GetAll();
            this.materiasList.DataTextField = "descripcion";
            this.materiasList.DataBind();
            CargarComisiones();
        }

        private void ClearForm()
        {
            this.descripcionTextBox.Text = string.Empty;
            this.anioTextBox.Text = string.Empty;
            this.cupoTextBox.Text = string.Empty;
            this.comisionesList.SelectedIndex = 0;
            this.materiasList.SelectedIndex = 0;
        }
        
        private void LoadEntity(Curso curso)
        {
            curso.Descripcion = this.descripcionTextBox.Text;
            curso.AnioCalendario = Convert.ToInt32(this.anioTextBox.Text);
            curso.IDMateria = new MateriaLogic().DameIDMateria(this.materiasList.SelectedIndex);
                        
            if (this.FormMode == FormModes.Alta)
            {
                curso.IDComision = new ComisionLogic().DameIDComision(this.comisionesList.SelectedIndex, curso.IDMateria);
            }
            else
            {
                curso.IDComision = new ComisionLogic().DameIDComision(this.comisionesList.SelectedIndex);
            }

            curso.Cupo = Convert.ToInt32(this.cupoTextBox.Text);
        }
        
        private void SaveEntity(Curso curso)
        {
            this.Logic.Save(curso);
        }

        private void CargarComisiones()
        {
            if (((Usuario)Session["logueo"]).Per.TipoPersona == Business.Entities.Personas.TiposPersonas.Administrador && this.FormMode == FormModes.Alta)
            {
                this.comisionesList.DataSource = new ComisionLogic().GetAllXPlan(new MateriaLogic().DameIDPlanMateria(this.materiasList.SelectedIndex));

            }
            else if (this.FormMode == FormModes.Baja || this.FormMode == FormModes.Modificacion)
            {
                this.comisionesList.DataSource = new ComisionLogic().GetAll();
            }
            else
            {
                this.comisionesList.DataSource = new ComisionLogic().GetAllXPlan(((Usuario)Session["logueo"]).Per.IDPlan);
            }
            this.comisionesList.DataTextField = "descripcion";
            this.comisionesList.DataBind();
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
                    this.FormMode = FormModes.Baja;
                    LoadGrid();
                    CargarListas();
                    CargarComisiones();
                    this.adminPanel.Visible = true;
                    this.usuarioPanel.Visible = false;
                    this.gridPanel.Visible = true;
                    if (((Usuario)Session["logueo"]).Per.TipoPersona == Business.Entities.Personas.TiposPersonas.Docente)
                    {
                        this.gridActionPanel.FindControl("nuevoLinkButton").Visible = false;
                        this.gridActionPanel.FindControl("editarLinkButton").Visible = false;
                        this.gridActionPanel.FindControl("eliminarLinkButton").Visible = false;
                    }
                    else
                    {
                        this.gridActionPanel.Visible = false;
                    }
                    this.gridView.Columns[5].Visible = false;
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
                    this.eliminarLinkButton.Visible = false;
                    this.editarLinkButton.Visible = false;
                    LoadGrid();
                    CargarListas();
                    CargarComisiones();
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
            this.eliminarLinkButton.Visible = true;
            this.editarLinkButton.Visible = true;
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
                        this.Entity = new Curso();
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
                        this.Entity = new Curso();
                        this.LoadEntity(this.Entity);
                        this.SaveEntity(this.Entity);
                        this.LoadGrid();
                        break;
                    }
                default:
                    break;
            }

            Response.Redirect("Cursos.aspx");
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
                Response.Redirect("Cursos.aspx");
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void reporteLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReporteCursos.aspx");
        }

        protected void materiasList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.FormMode != FormModes.Baja || this.FormMode != FormModes.Modificacion)
            {
                CargarComisiones();
            }
        }
        
        #endregion
    }
}